using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;

namespace Gibs.Api.Controllers
{
    [Route("api/claims")]
    public class ClaimsController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet]
        public async Task<IEnumerable<ClaimResponse>> ListClaims(
            [FromQuery] PaginationQuery paging,
            [FromQuery] DateRangeQuery<DateOnly> notifyRange)       
        {
            var query = UoW.Claims.AsNoTracking();

            if (notifyRange.CanSearch)
                query = query.Where(x => x.NotifyDate >= notifyRange.DateFrom &&
                                         x.NotifyDate <= notifyRange.DateTo);

            return await PagedList(query, paging, claim => new ClaimResponse(claim));
        }

        [HttpGet("{claimNo}")]
        public async Task<ClaimResponse> GetClaim(string claimNo)
        {
            //notificationNo contains / forward slashes
            claimNo = System.Web.HttpUtility.UrlDecode(claimNo);

            var claim = await UoW.Claims
                                 .Where(x => x.ClaimNo  == claimNo 
                                          || x.NotifyNo == claimNo)
                                 .SingleOrDefaultAsync()
                ?? throw new NotFoundException($"Claim/Notification No [{claimNo}] was not found");
            
            return new ClaimResponse(claim);
        }

        [HttpPost]
        public async Task<ClaimResponse> RegisterClaim(RegisterClaimRequest dto)
        {
            var dn = await VerifyDebitNote(dto.PolicyNo, dto.LossDate);

            var product = await UoW.Products.FindAsync(dn.History.Policy.Members.ProductId);
            var codeFactory = UoW.GetCodeFactory(ApiDefaults.HqBranch, product);

            var c = new ClaimNotify(codeFactory, dn, 
                dto.NotifyDate, dto.LossDate, dto.Description);

            UoW.Claims.Add(c);
            await UoW.SaveChangesAsync();

            //var uri = new Uri($"{Request.Path}/{n.NotificationNo}", UriKind.Relative);
            //return Created(uri, new ClaimResponse(n));
            return new ClaimResponse(c);
        }

        private async Task<DebitNote> VerifyDebitNote(string policyNo, DateOnly lossDate)
        {
            var dnotes = await UoW.DebitNotes.AsNoTracking()
                                  .Where(x => x.PolicyNo == policyNo)
                                  .ToListAsync();
            if (dnotes.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(policyNo), 
                    "Invalid claim request. Policy does not exist");

            var dn = dnotes.Where(x => x.History.Business.StartDate <= lossDate)
                           .Where(x => x.History.Business.EndDate >= lossDate)
                           .FirstOrDefault() 
                ?? throw new ArgumentOutOfRangeException(nameof(policyNo), 
                "Policy is not active within claim period");
            return dn;
        }
    }
}
