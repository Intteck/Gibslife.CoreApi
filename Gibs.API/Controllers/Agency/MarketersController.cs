using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Gibs.Api.Controllers
{
    [Route("api/marketers")]
    public class MarketersController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet("{marketerId}")]
        public async Task<MarketerResponse> GetMarketer(string marketerId)
        {
            var marketer = await UoW.Marketers.FindAsync(marketerId)
               ?? throw new NotFoundException($"Marketer [{marketerId}] was not found");

            return new MarketerResponse(marketer);
        }

        [HttpGet]
        public async Task<IEnumerable<MarketerResponse>> ListMarketers(
            [FromQuery] PaginationQuery paging)
        {
            var query = UoW.Marketers.OrderBy(x => x.Id).AsNoTracking();

            return await PagedList(query, paging, marketer => new MarketerResponse(marketer));
        }

        [HttpPost]
        public async Task<MarketerResponse> CreateMarketer(CreateMarketerRequest dto)
        {
            var marketer = new Marketer(dto.MarketerID, dto.FullName, dto.ChannelID, dto.SubChannelID);
            UoW.Marketers.Add(marketer);

            await UoW.SaveChangesAsync();

            return new MarketerResponse(marketer);
        }

        [HttpPost("{marketerId}")]
        public async Task<MarketerResponse> UpdateMarketer(string marketerId, UpdateMarketerRequest dto)
        {
            var marketer = await UoW.Marketers.FindAsync(marketerId)
               ?? throw new NotFoundException($"Marketer [{marketerId}] was not found");

            //marketer.UpdateMarketer();
            await UoW.SaveChangesAsync();

            return new MarketerResponse(marketer);
        }

        [HttpDelete("{marketerId}")]
        public async Task DeleteMarketer(string marketerId)
        {
            var marketer = await UoW.Marketers.FindAsync(marketerId)
               ?? throw new NotFoundException($"Marketer [{marketerId}] was not found");

            UoW.Remove(marketer);
            await UoW.SaveChangesAsync();
        }
    }
}
