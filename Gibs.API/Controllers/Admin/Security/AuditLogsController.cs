using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;

namespace Gibs.Api.Controllers
{
    [Route("api/audit-logs")]
    public class AuditLogsController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet]
        public async Task<IEnumerable<AuditLogResponse>> ListAuditLogs(
            [FromQuery] PaginationQuery paging)
        {
            var query = UoW.AuditLogs.OrderByDescending(x => x.Id).AsNoTracking();

            return await PagedList(query, paging, auditLog => new AuditLogResponse(auditLog));
        }
    }
}
