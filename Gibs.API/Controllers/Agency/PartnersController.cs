using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;

namespace Gibs.Api.Controllers
{
    [Route("api/partners")]
    public class PartnersController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet("category/{categoryID}")]
        public async Task<IEnumerable<PartnerResponse>> ListPartnersByCategory(
            [FromQuery] PaginationQuery paging,
            [FromQuery] StringSearchQuery search,
            [FromRoute] string categoryID)
        {
            var query = UoW.Parties.Include(x => x.PartyType)
                                   .Where(x => x.PartyType.CategoryId == categoryID)
                                   .OrderBy(x => x.Id)
                                   .AsNoTracking();
            if (search.CanSearch)
                query = query.Where(x => x.FullName.StartsWith(search.SearchText));

            return await PagedList(query, paging, partner => new PartnerResponse(partner));
        }

        [HttpGet]
        public async Task<IEnumerable<PartnerResponse>> ListPartners(
            [FromQuery] PaginationQuery paging,
            [FromQuery] StringSearchQuery search,
            [FromQuery] string? partnerTypeID)
        {
            var query = UoW.Parties.OrderBy(x => x.Id).AsNoTracking();

            if (search.CanSearch)
                query = query.Where(x => x.FullName.StartsWith(search.SearchText));

            if (partnerTypeID != null)
                query = query.Where(x => x.TypeId == partnerTypeID);

            return await PagedList(query, paging, partner => new PartnerResponse(partner));
        }

        [HttpGet("{partnerId}")]
        public async Task<PartnerResponse> GetPartner(string partnerId)
        {
            var partner = await UoW.Parties.FindAsync(partnerId) 
                ?? throw new NotFoundException($"Partner [{partnerId}] was not found");

            return new PartnerResponse(partner);
        }

        [HttpPost]
        public async Task<PartnerResponse> CreatePartner(CreatePartnerRequest dto)
        {
            var partner = new Party(null, null, dto.FullName, dto.Email, dto.Phone);
            UoW.Parties.Add(partner);

            await UoW.SaveChangesAsync();

            return new PartnerResponse(partner);
        }

        [HttpPost("{partnerId}")]
        public async Task<PartnerResponse> UpdatePartner(string partnerId, UpdatePartnerRequest dto)
        {
            var partner = await UoW.Parties.FindAsync(partnerId)
               ?? throw new NotFoundException($"Partner [{partnerId}] was not found");

            //partner.UpdatePersonal(dto.CountryID, dto.DateOfBirth, dto.NextOfKin);
            partner.UpdateContact(dto.Street, dto.CityLGA, dto.StateID, dto.Phone, dto.PhoneAlt);
            partner.UpdateKyc(dto.TaxNumber, dto.KycTypeID, dto.KycNumber, dto.KycIssueDate, dto.KycExpiryDate);
            await UoW.SaveChangesAsync();

            return new PartnerResponse(partner);
        }

        [HttpDelete("{partnerId}")]
        public async Task DeletePartner(string partnerId)
        {
            var partner = await UoW.Parties.FindAsync(partnerId)
               ?? throw new NotFoundException($"Partner [{partnerId}] was not found");

            //UoW.Remove(partner);
            await UoW.SaveChangesAsync();
        }

    }
}
