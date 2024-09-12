using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;

using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;

namespace Gibs.Api.Controllers
{
    [Route("api/policies")]
    public class PoliciesController(ControllerServices services) : SecureControllerBase(services)
    {
        private static readonly SemaphoreSlim Mutex = new(1);
        private static PolicyResponse DtoConverter(PolicyHistory h) => new(h);

        [HttpGet]
        public async Task<IEnumerable<PolicyResponse>> ListPolicies(
            [FromQuery] string classID,
            [FromQuery] PaginationQuery paging,
            [FromQuery] DateRangeQuery<DateTime> createdRange,
            [FromQuery] DateRangeQuery<DateOnly> expiresRange,
            [FromQuery] string? policyNo = null,
            [FromQuery] string? insuredName = null)
        {
            IQueryable<PolicyHistory> query = UoW.Policies.AsNoTracking()
                                                 .SelectMany(x => x.Histories)
                                                 .Include(x => x.Policy).ThenInclude(x => x.Product)
                                                 .Where(x => x.Policy.Product.ClassId == classID.Trim())
                                                 .OrderByDescending(x => x.SerialNo);
            if (createdRange.CanSearch)
                query = query.Where(x => x.Business.TransDate >= createdRange.DateFrom &&
                                         x.Business.TransDate <= createdRange.DateTo);
            if (expiresRange.CanSearch)
                query = query.Where(x => x.Business.EndDate >= expiresRange.DateFrom &&
                                         x.Business.EndDate <= expiresRange.DateTo);
            // .Contains() is slow
            if (!string.IsNullOrWhiteSpace(insuredName))
                query = query.Where(x => x.Members.CustomerName.StartsWith(insuredName.Trim()));

            if (!string.IsNullOrWhiteSpace(policyNo))
                query = query.Where(x => x.PolicyNo == policyNo.Trim());

            return await PagedList(query, paging, DtoConverter);
        }

        [HttpGet("{policyNo}")]
        public async Task<PolicyResponse> GetPolicy(string policyNo)
        {
            //policyNo contains / forward slashes
            policyNo = System.Web.HttpUtility.UrlDecode(policyNo);

            var policy = await UoW.Policies.AsSplitQuery()
                                  .Where(p => p.PolicyNo == policyNo)
                                  .Include(p => p.Histories).ThenInclude(h => h.DebitNote)
                                  .Include(p => p.Histories).ThenInclude(h => h.Sections)
                                  .Include(p => p.Product)         
                                  .FirstOrDefaultAsync() 
                ?? throw new NotFoundException($"Policy [{policyNo}] was not found");

            return DtoConverter(policy.GetCurrentHistory());
        }

        [HttpGet("sections")]
        public async Task<PolicyResponse> GetPolicyBySection(
            [FromQuery, Required] string fieldName,
            [FromQuery, Required] string fieldValue)
        {
            if (!string.Equals(fieldName, "VehicleRegNo", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException($"Unknown FieldName [{fieldName}]", nameof(fieldName));

            if (string.IsNullOrWhiteSpace(fieldValue))
                throw new ArgumentNullException(nameof(fieldValue), "FieldValue cannot be empty");

            var value = fieldValue;
            var policies = await UoW.Policies.AsNoTracking()
                                  //.Include(p => p.Histories).ThenInclude(h => h.DebitNote)
                                    .Include(p => p.Histories).ThenInclude(h => h.Sections)
                                    .Include(p => p.Product)
                                    .Where(p => p.Histories.Any(
                                                h => h.Sections.Any(
                                                    s => s.ExFields.Field1 == value)))
                                    .ToListAsync();
            if (policies.Count == 0)
                throw new NotFoundException($"Policy having [{fieldName.ToUpper()}/{fieldValue}] was not found");

            if (policies.Count == 1)
                return DtoConverter(policies[0].GetCurrentHistory());

            //take the latest
            var policy = policies.OrderByDescending(x => x.SerialNo).First();

            return DtoConverter(policy.GetCurrentHistory());
        }
        
        [HttpPost]
        public async Task<PolicyResponse> CreatePolicy(CreatePolicyRequest dto)
        {
            try
            {
                var product = await UoW.Products
                                       .Where(x => x.Id == dto.ProductID)
                                       .Include(x => x.Sections).ThenInclude(x => x.SMIs)
                                       .FirstOrDefaultAsync() 
                    ?? throw new ArgumentException("ProductID is invalid", nameof(dto.ProductID));
                
                if (string.IsNullOrWhiteSpace(dto.PartnerID))
                    dto.PartnerID = ApiDefaults.AgentID;

                var party = await UoW.Parties.FindAsync(dto.PartnerID) 
                    ?? throw new ArgumentException("AgentID is invalid", nameof(dto.PartnerID));

                //only one thread is allowed at a time 
                await Mutex.WaitAsync().ConfigureAwait(false);

                var policy = await CreateNewPolicyAsync(dto, product);
                UoW.Policies.Add(policy);

                //var rcp = await CreatePaymentRecieptAsync(
                //    dto.PaymentAccountID, dto.PaymentReferenceID, policy.Current, party);
                //UoW.Receipts.Add(rcp);

                await UoW.SaveChangesAsync();

                //var uri = new Uri($"{Request.Path}/{policy.PolicyNo}", UriKind.Relative);
                //return Created(uri, DtoConverter(policy.Current));
                return DtoConverter(policy.GetCurrentHistory());
            }
            finally
            {
                Mutex.Release();
            }
        }

        [HttpPost("{policyNo}/renew")]
        public async Task<PolicyResponse> RenewPolicy(string policyNo, RenewPolicyRequest dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.PartnerID))
                    dto.PartnerID = ApiDefaults.AgentID;

                var party = await UoW.Parties.FindAsync(dto.PartnerID) 
                    ?? throw new ArgumentException("AgentID is invalid", nameof(dto.PartnerID));

                //policyNo contains forward slashes /
                policyNo = System.Web.HttpUtility.UrlDecode(policyNo);
                var policy = await UoW.Policies
                                      .Where(p => p.PolicyNo == policyNo)
                                      .Include(p => p.Product).ThenInclude(p => p.Sections).ThenInclude(s => s.SMIs)
                                      .Include(p => p.Customer)
                                      .Include(p => p.Histories).ThenInclude(h => h.Sections)
                                      .AsSplitQuery()
                                      .FirstOrDefaultAsync() 
                    ?? throw new NotFoundException($"Policy [{policyNo}] was not found");

                var partyTypes = await GetAllPartyTypes();

                var fxCurrency = await UoW.Currencies.FindAsync(dto.CurrencyID);
                var rates = policy.Product.GetCommissionRate(party, partyTypes);
                var codeFactory = UoW.GetCodeFactory(ApiDefaults.Branch, policy.Product);

                var business = new BusinessBasics(
                    businessType: BusinessTypeEnum.DIRECT, //no-coinsurance

                    party: party,
                    //sourceType: dto.BusinessSource == null
                    //            ? party.Type.ToSourceTypeEnum()
                    //            : dto.BusinessSource.Value,
                    startDate: dto.StartDate,
                    endDate: dto.EndDate,
                    standardCoverDays: dto.StandardCoverDays,
                    ourShareRate: 100,
                    flatPremiumRate: 0,
                    commission: rates,
                    localCurrency: ApiDefaults.LocalCurrency,
                    fxCurrency: fxCurrency,
                    fxRate: dto.CurrencyRate);

                //only one thread is allowed at a time 
                await Mutex.WaitAsync().ConfigureAwait(false);

                var ph = policy.DoRenew(codeFactory, business);
                //var rcp = await CreatePaymentRecieptAsync(
                //    dto.PaymentAccountID, dto.PaymentReferenceID, ph, party);

                //UoW.Receipts.Add(rcp);

                await UoW.SaveChangesAsync();

                //var uri = new Uri($"{Request.Path}/{policy.PolicyNo}", UriKind.Relative);
                //return Created(uri, DtoConverter(ph));
                return DtoConverter(ph);
            }
            finally
            {
                Mutex.Release();
            }
        }

        [HttpPost("{policyNo}/endorse")]
        public async Task<PolicyResponse> EndorsePolicy(string policyNo)
        {
            //policyNo contains forward slashes /
            policyNo = System.Web.HttpUtility.UrlDecode(policyNo);
            var policy = await UoW.Policies
                                  .Where(p => p.PolicyNo == policyNo)
                                  .FirstOrDefaultAsync() 
                ?? throw new NotFoundException($"Policy [{policyNo}] was not found");

            throw new NotImplementedException("Feature coming soon");
        }

        [HttpPost("{policyNo}/cancel")]
        public async Task<string> CancelPolicy(string policyNo)
        {
            //policyNo contains forward slashes /
            policyNo = System.Web.HttpUtility.UrlDecode(policyNo);
            var policy = await UoW.Policies
                                  .Where(p => p.PolicyNo == policyNo)
                                  .FirstOrDefaultAsync() 
                ?? throw new NotFoundException($"Policy [{policyNo}] was not found");

            throw new NotImplementedException("Feature coming soon");
        }


        #region Private Functions
        private async Task<Policy> CreateNewPolicyAsync(CreatePolicyRequest dto, Product product)
        {
            var fxCurrency = await UoW.Currencies.FindAsync(dto.CurrencyID);
            var party = await UoW.Parties.FindAsync(dto.PartnerID);
            var customer = await GetOrAddCustomer(dto.Insured);
            var codeFactory = UoW.GetCodeFactory(ApiDefaults.Branch, product);

            if (party is null)
                throw new ArgumentException("AgentID is invalid", nameof(dto.PartnerID));

            var partyTypes = await GetAllPartyTypes();

            var rates = product.GetCommissionRate(party, partyTypes);
            //var marketer = new Marketer(ApiDefaults.MarketerID, party.FullName, null, null);

            //TODO, use sub channel id provided to fetch channel
            dto.SubChannelID ??= ApiDefaults.SubChannelID; 
            var channel = await UoW.SalesChannels.FindAsync(ApiDefaults.ChannelID);  
            var subChannel = await UoW.SalesSubChannels.FindAsync(dto.SubChannelID);       

            if (channel is null)
                throw new ArgumentException("ChannelID is invalid");

            if (subChannel is null)
                throw new ArgumentException("SubChannelID is invalid", nameof(dto.SubChannelID));

            var business = new BusinessBasics(
                businessType: BusinessTypeEnum.DIRECT,

                party: party,
                //sourceType: dto.BusinessSource == null 
                //            ? party.Type.ToSourceTypeEnum() 
                //            : dto.BusinessSource.Value,
                startDate: dto.StartDate,
                endDate: dto.EndDate,
                standardCoverDays: dto.StandardCoverDays,
                ourShareRate: 100,
                commission: rates,
                flatPremiumRate: 0,
                localCurrency: ApiDefaults.LocalCurrency,
                fxCurrency: fxCurrency,
                fxRate: dto.CurrencyRate);

            var marketer = new Marketer(ApiDefaults.MarketerID, party.FullName, null, null);

            // no need to send channel AND sub-channel, send only sub-channel
            return new Policy(codeFactory, business, product, customer, ApiDefaults.Branch,
                 party, marketer, channel, subChannel, dto.AsRecords());
        }

        private async Task<string[]> GetAllPartyTypes()
        {
            return await UoW.PartyTypes.Select(x => x.Id).ToArrayAsync();
        }
        #endregion
    }
}
