using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gibs.Api.Controllers
{
    [Route("api/clauses/motor")]
    public class ClausesMotorController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet("{clauseId}")]
        public async Task<MotorClauseResponse> GetClause(string clauseId)
        {
            var clause = await UoW.MotorClauses.FindAsync(clauseId)
               ?? throw new NotFoundException($"Clauses [{clauseId}] was not found");

            return new MotorClauseResponse(clause);
        }

        [HttpGet]
        public async Task<IEnumerable<MotorClauseResponse>> ListClauses(
            [FromQuery] PaginationQuery paging)
        {
            var query = UoW.MotorClauses.OrderBy(x => x.Id).AsNoTracking();

            return await PagedList(query, paging, clause => new MotorClauseResponse(clause));
        }

       [HttpPost]
        public async Task<MotorClauseResponse> CreateClause(CreateMotorClauseRequest dto)
        {
            var clause = new MotorClause
            {
                Id = dto.ClauseID,
                ProductId = dto.ProductID,
                Category = dto.CoverTypeID,
                EntitledToA = dto.EntitledToA,
                EntitledToB = dto.EntitledToB,
                LimitationA = dto.LimitationA,
                LimitationB = dto.LimitationB, 
                VehicleUsage = dto.VehicleUsage,
            };

            UoW.MotorClauses.Add(clause);
            await UoW.SaveChangesAsync();

            return new MotorClauseResponse(clause);
        }

        [HttpPost("{clauseId}")]
        public async Task<MotorClauseResponse> UpdateClause(string clauseId, UpdateMotorClauseRequest dto)
        {
            var clause = await UoW.MotorClauses.FindAsync(clauseId)
               ?? throw new NotFoundException($"Clause [{clauseId}] was not found");

            clause.ProductId = dto.ProductID;
            clause.Category = dto.CoverTypeID;
            clause.EntitledToA = dto.EntitledToA;
            clause.EntitledToB = dto.EntitledToB;
            clause.LimitationA = dto.LimitationA;
            clause.VehicleUsage = dto.VehicleUsage;
            await UoW.SaveChangesAsync();

            return new MotorClauseResponse(clause);
        }
        
        [HttpDelete("{clauseId}")]
        public async Task DeleteClause(string clauseId)
        {
            var clause = await UoW.MotorClauses.FindAsync(clauseId)
               ?? throw new NotFoundException($"Clause [{clauseId}] was not found");

            UoW.Remove(clause);
            await UoW.SaveChangesAsync();
        }
    }
}