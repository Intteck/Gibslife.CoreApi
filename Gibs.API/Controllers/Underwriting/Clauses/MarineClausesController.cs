using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gibs.Api.Controllers
{
    [Route("api/clauses/marine")]
    public class ClausesMarineController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet("{clauseId}")]
        public async Task<MarineClauseResponse> GetClause(string clauseId)
        {
            var clause = await UoW.MarineClauses.FindAsync(clauseId)
               ?? throw new NotFoundException($"Clauses [{clauseId}] was not found");

            return new MarineClauseResponse(clause);
        }

        [HttpGet]
        public async Task<IEnumerable<MarineClauseResponse>> ListClauses(
            [FromQuery] PaginationQuery paging)
        {
            var query = UoW.MarineClauses.OrderBy(x => x.Id).AsNoTracking();

            return await PagedList(query, paging, clause => new MarineClauseResponse(clause));
        }

        [HttpPost]
        public async Task<MarineClauseResponse> CreateClause(CreateMarineClauseRequest dto)
        {
            var clause = new MarineClause
            {
                Id = dto.ClauseID,
                Category = dto.CategoryID,
                Details = dto.Details,
                ICC = dto.ICC,
            };
            UoW.MarineClauses.Add(clause);

            await UoW.SaveChangesAsync();

            return new MarineClauseResponse(clause);
        }

        [HttpPost("{clauseId}")]
        public async Task<MarineClauseResponse> UpdateClause(string clauseId, UpdateMarineClauseRequest dto)
        {
            var clause = await UoW.MarineClauses.FindAsync(clauseId)
               ?? throw new NotFoundException($"Clause [{clauseId}] was not found");

            clause.Category = dto.CategoryID;
            clause.Details = dto.Details;
            clause.ICC = dto.ICC;
            await UoW.SaveChangesAsync();

            return new MarineClauseResponse(clause);
        }

        [HttpDelete("{clauseId}")]
        public async Task DeleteClause(string clauseId)
        {
            var clause = await UoW.MarineClauses.FindAsync(clauseId)
               ?? throw new NotFoundException($"Clause [{clauseId}] was not found");

            UoW.Remove(clause);
            await UoW.SaveChangesAsync();
        }
    }
}