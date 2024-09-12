//using System.Linq;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;
//using Gibs.Api.Contracts.V1;
//using Gibs.Domain.Entities;

//namespace Gibs.Api.Controllers
//{
//    [Route("api/branches")]
//    public class BranchesController(ControllerServices services) : SecureControllerBase(services)
//    {
//        [HttpGet("{branchId}")]
//        public async Task<BranchResponse> GetBranch(string branchId)
//        {
//            var branch = await UoW.Branches.FindAsync(branchId)
//               ?? throw new NotFoundException($"Branch [{branchId}] was not found");

//            return new BranchResponse(branch);
//        }

//        [HttpGet]
//        public async Task<IEnumerable<BranchResponse>> ListBranches(
//            [FromQuery] PaginationQuery paging)
//        {
//            var query = UoW.Branches.OrderBy(x => x.Name).AsNoTracking();

//            return await PagedList(query, paging, branch => new BranchResponse(branch));
//        }

//        [HttpPost]
//        public async Task<BranchResponse> CreateBranch(CreateBranchRequest dto)
//        {
//            var branch = new Branch(dto.BranchID, dto.BranchName, dto.BranchAltName, dto.StateID, dto.RegionID);
//            UoW.Branches.Add(branch);

//            await UoW.SaveChangesAsync();

//            return new BranchResponse(branch);
//        }

//        [HttpPost("{branchId}")]
//        public async Task<BranchResponse> UpdateBranch(string branchId, UpdateBranchRequest dto)
//        {
//            var branch = await UoW.Branches.FindAsync(branchId)
//               ?? throw new NotFoundException($"Branch [{branchId}] was not found");

//            //branch.UpdateBranch(dto.Address, dto.Phone);
//            await UoW.SaveChangesAsync();

//            return new BranchResponse(branch);
//        }

//        [HttpDelete("{branchId}")]
//        public async Task DeleteBranch(string branchId)
//        {
//            var branch = await UoW.Branches.FindAsync(branchId)
//               ?? throw new NotFoundException($"Region [{branchId}] was not found");

//            UoW.Remove(branch);
//            await UoW.SaveChangesAsync();
//        }

//    }
//}
