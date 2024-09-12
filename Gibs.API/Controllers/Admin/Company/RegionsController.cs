using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;

namespace Gibs.Api.Controllers
{
    [Route("api/regions")]
    public class RegionsController(ControllerServices services) : SecureControllerBase(services)
    {
        //[HttpGet("{regionId}")]
        //public async Task<RegionResponse> GetRegion(string regionId)
        //{
        //    var region = await UoW.Regions.FindAsync(regionId)
        //       ?? throw new NotFoundException($"Region [{regionId}] was not found");

        //    return new RegionResponse(region);
        //}

        [HttpGet]
        public async Task<IEnumerable<RegionResponse>> ListRegions()
        {
            var query = UoW.Regions.Include(x => x.Branches)
                                   .OrderBy(x => x.Name)
                                   .AsNoTracking();

            var paging = new PaginationQuery(200);
            return await PagedList(query, paging, region => new RegionResponse(region));
        }

        [HttpPost]
        public async Task<RegionResponse> CreateRegion(CreateRegionRequest dto)
        {
            var region = new Region(dto.RegionID, dto.RegionName, dto.RegionAltName);
            UoW.Regions.Add(region);

            await UoW.SaveChangesAsync();

            return new RegionResponse(region);
        }

        [HttpPost("{regionId}")]
        public async Task<RegionResponse> UpdateRegion(
            [FromRoute] string regionId,
            [FromBody] UpdateRegionRequest dto)
        {
            var region = await UoW.Regions.FindAsync(regionId)
               ?? throw new NotFoundException($"Region [{regionId}] was not found");

            region.UpdateRegionName(dto.RegionName);
            await UoW.SaveChangesAsync();

            return new RegionResponse(region);
        }

        [HttpDelete("{regionId}")]
        public async Task DeleteRegion(
            [FromRoute] string regionId)
        {
            var region = await UoW.Regions.FindAsync(regionId)
               ?? throw new NotFoundException($"Region [{regionId}] was not found");

            UoW.Remove(region);
            await UoW.SaveChangesAsync();
        }

        [HttpPost("{regionId}/branches")]
        public async Task<BranchResponse> CreateBranch(
            [FromRoute] string regionId,
            [FromBody] CreateBranchRequest dto)
        {
            var region = await UoW.Regions.FindAsync(regionId)
               ?? throw new NotFoundException($"Region [{regionId}] was not found");

            var branch = new Branch(dto.BranchID, dto.BranchName, dto.BranchAltName, dto.StateID, regionId);
            region.Branches.Add(branch);

            await UoW.SaveChangesAsync();

            return new BranchResponse(branch);
        }

        [HttpPost("{regionId}/branches/{branchId}")]
        public async Task<BranchResponse> UpdateBranch(
            [FromRoute] string regionId,
            [FromRoute] string branchId, 
            [FromBody] UpdateBranchRequest dto)
        {
            var region = await UoW.Regions.Include(x => x.Branches)
                                          .SingleOrDefaultAsync(x => x.Id == regionId)
               ?? throw new NotFoundException($"Region [{regionId}] was not found");

            var branch = region.Branches.Where(x => x.Id == branchId).FirstOrDefault()
               ?? throw new NotFoundException($"Branch [{branchId}] was not found");

            //branch.UpdateBranch(dto.Address, dto.Phone);
            await UoW.SaveChangesAsync();

            return new BranchResponse(branch);
        }

        [HttpDelete("{regionId}/branches/{branchId}")]
        public async Task DeleteBranch(
            [FromRoute] string regionId,
            [FromRoute] string branchId)
        {
            var region = await UoW.Regions.Include(x => x.Branches)
                                          .SingleOrDefaultAsync(x => x.Id == regionId)
               ?? throw new NotFoundException($"Region [{regionId}] was not found");

            var branch = region.Branches.Where(x => x.Id == branchId).FirstOrDefault()
               ?? throw new NotFoundException($"Region [{branchId}] was not found");

            region.Branches.Remove(branch);
            await UoW.SaveChangesAsync();
        }

    }
}
