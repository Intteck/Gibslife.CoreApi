using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;

namespace Gibs.Api.Controllers
{
    [Route("api/channels")]
    public class ChannelsController(ControllerServices services) : SecureControllerBase(services)
    {
        //[HttpGet("{channelId}")]
        //public async Task<ChannelResponse> GetChannel(string channelId)
        //{
        //    var channel = await UoW.SalesChannels.FindAsync(channelId)
        //       ?? throw new NotFoundException($"Channel [{channelId}] was not found");

        //    return new ChannelResponse(channel);
        //}

        [HttpGet]
        public async Task<IEnumerable<ChannelResponse>> ListChannels()
        {
            var query = UoW.SalesChannels.OrderBy(x => x.Name)
                                         .AsNoTracking();

            var paging = new PaginationQuery(200);
            return await PagedList(query, paging, channel => new ChannelResponse(channel));
        }

        [HttpPost]
        public async Task<ChannelResponse> CreateChannel(CreateChannelRequest dto)
        {
            var channel = new SalesChannel(dto.BranchID, 
                dto.ChannelID, dto.ChannelName, dto.ChannelAltName); 
     
            UoW.SalesChannels.Add(channel);
            await UoW.SaveChangesAsync();

            return new ChannelResponse(channel);
        }

        [HttpPost("{channelId}")]
        public async Task<ChannelResponse> UpdateChannel(string channelId, UpdateChannelRequest dto)
        {
            var channel = await UoW.SalesChannels.FindAsync(channelId)
               ?? throw new NotFoundException($"Channel [{channelId}] was not found");

            channel.Update(dto.ChannelName, dto.ChannelAltName);
            await UoW.SaveChangesAsync();

            return new ChannelResponse(channel);
        }

        [HttpDelete("{channelId}")]
        public async Task DeleteChannel(string channelId)
        {
            var channel = await UoW.SalesChannels.FindAsync(channelId)
               ?? throw new NotFoundException($"Channel [{channelId}] was not found");

            UoW.Remove(channel);
            await UoW.SaveChangesAsync();
        }
    }
}