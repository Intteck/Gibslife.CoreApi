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
    [Route("api/risks")]
    public class RisksController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet]
        public async Task<IEnumerable<RiskResponse>> ListRisks()
        {
            var query = UoW.Classes.Include(x => x.MidClasses)
                                   .ThenInclude(x => x.Products)
                                   .OrderBy(x => x.Name)
                                   .AsNoTracking();

            var paging = new PaginationQuery(200);
            return await PagedList(query, paging, risk => new RiskResponse(risk));
        }

        [HttpGet("{riskId}/sub")]
        public async Task<IEnumerable<MidRiskResponse>> ListMidRisks(
            [FromRoute] string riskId)
        {
            var query = UoW.Classes.Include(x => x.MidClasses)
                                   .ThenInclude(x => x.Products)
                                   .Where(x => x.Id == riskId)
                                   .SelectMany(x => x.MidClasses)
                                   .OrderBy(x => x.Name)
                                   .AsNoTracking();

            var paging = new PaginationQuery(200);
            return await PagedList(query, paging, mid => new MidRiskResponse(mid));
        }

        [HttpPost("{riskId}/sub")]
        public async Task<MidRiskResponse> CreateMidRisk(
            [FromRoute] string riskId, 
            CreateMidRiskRequest dto)
        {
            var risk = await UoW.Classes.FindAsync(riskId)
               ?? throw new NotFoundException($"Class [{riskId}] was not found");

            var midRisk = new MidClass(risk, dto.MidRiskID, dto.MidRiskName);
            risk.MidClasses.Add(midRisk);

            await UoW.SaveChangesAsync();
            return new MidRiskResponse(midRisk);
        }

        [HttpPost("{riskId}/sub/{midRiskId}")]
        public async Task<MidRiskResponse> UpdateMidRisk(
            [FromRoute] string riskId, 
            [FromRoute] string midRiskId, 
            UpdateMidRiskRequest dto)
        {
            var midRisk = await UoW.Classes
                                   .Where(x => x.Id == riskId)
                                   .Include(x => x.MidClasses.Where(x => x.Id == midRiskId))
                                   .SelectMany(x => x.MidClasses)
                                   .FirstOrDefaultAsync()
                            ?? throw new NotFoundException($"MidRisk [{midRiskId}] was not found");

            midRisk.UpdateName(dto.MidRiskName);
            await UoW.SaveChangesAsync();

            return new MidRiskResponse(midRisk);
        }

        [HttpDelete("{riskId}/sub/{midRiskId}")]
        public async Task DeleteMidRisk(
            [FromRoute] string riskId, 
            [FromRoute] string midRiskId)
        {
            var midRisk = await UoW.Classes
                                   .Where(x => x.Id == riskId)
                                   .Include(x => x.MidClasses.Where(x => x.Id == midRiskId))
                                   .ThenInclude(x => x.Products)
                                   .SelectMany(x => x.MidClasses)
                                   .FirstOrDefaultAsync()
                            ?? throw new NotFoundException($"MidRisk [{midRiskId}] was not found");

            if (midRisk.Products.Count != 0)
                throw new Exception("Cannot delete a MidRisk that has products");

            UoW.Remove(midRisk);
            //await UoW.SaveChangesAsync();
        }
    }
}
