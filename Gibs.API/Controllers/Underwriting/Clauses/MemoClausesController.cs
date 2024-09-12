using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gibs.Api.Controllers
{
    [Route("api/clauses/memo")]
    public class ClausesMemoController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet("{clauseId}")]
        public async Task<MemoClauseResponse> GetClause(string clauseId)
        {
            var clause = await UoW.MemoClauses.FindAsync(clauseId)
               ?? throw new NotFoundException($"Clauses [{clauseId}] was not found");

            return new MemoClauseResponse(clause);
        }

        [HttpGet]
        public async Task<IEnumerable<MemoClauseResponse>> ListClauses(
            [FromQuery] PaginationQuery paging)
        {
            var query = UoW.MemoClauses.OrderBy(x => x.Id).AsNoTracking();

            return await PagedList(query, paging, clause => new MemoClauseResponse(clause));
        }

        [HttpPost]
        public async Task<MemoClauseResponse> CreateClause(CreateMemoClauseRequest dto)
        {
            var clause = new MemoClause
            {
                Id = dto.ClauseID,
                ProductId = dto.ProductID,
                Category = dto.CategoryID, 
                Document1 = dto.Document1,
                Document2 = dto.Document2,
                HeaderText = dto.HeaderText, 
            };

            UoW.MemoClauses.Add(clause);
            await UoW.SaveChangesAsync();

            return new MemoClauseResponse(clause);
        }

        [HttpPost("{clauseId}")]
        public async Task<MemoClauseResponse> UpdateClause(string clauseId, UpdateMemoClauseRequest dto)
        {
            var clause = await UoW.MemoClauses.FindAsync(clauseId)
               ?? throw new NotFoundException($"Clause [{clauseId}] was not found");

            clause.ProductId = dto.ProductID;
            clause.Category = dto.CategoryID;
            clause.Document1 = dto.Document1;
            clause.Document2 = dto.Document2;
            clause.HeaderText = dto.HeaderText;

            await UoW.SaveChangesAsync();

            return new MemoClauseResponse(clause);
        }

        [HttpDelete("{clauseId}")]
        public async Task DeleteClause(string clauseId)
        {
            var clause = await UoW.MemoClauses.FindAsync(clauseId)
               ?? throw new NotFoundException($"Clause [{clauseId}] was not found");

            UoW.Remove(clause);
            await UoW.SaveChangesAsync();
        }
    }
}