//using Gibs.Api.Contracts.V1;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Gibs.Api.Controllers
//{
//    [Route("api/codes")]
//    public class CodeNumberController(ControllerServices services) : SecureControllerBase(services)
//    {
//        [HttpGet]
//        public async Task<IEnumerable<CodeNumberResponse>> ListCodeNumbers(
//            [FromQuery] PaginationQuery paging)
//        {
//            var query = UoW.CodeNumbers.OrderBy(x => x.CodeNumberID).AsNoTracking();

//            return await PagedList(query, paging, codeNumber => new CodeNumberResponse(codeNumber));
//        }

//        [HttpPost("{codeNumberId}")]
//        public async Task<CodeNumberResponse> UpdateCodeNumber(string CodeNumberId, UpdateCodeNumberRequest dto)
//        {
//            var codeNumber = await UoW.CodeNumbers.FindAsync(CodeNumberId)
//               ?? throw new NotFoundException($"CodeNumber [{CodeNumberId}] was not found");

//            //codeNumber.Update(dto.SerialIncrementField, dto.SerialResetMode, dto.Format, dto.Sample);
//            await UoW.SaveChangesAsync();

//            return new CodeNumberResponse(codeNumber);
//        }
//    }
//}
