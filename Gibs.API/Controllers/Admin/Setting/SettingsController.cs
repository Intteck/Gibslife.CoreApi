using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;

namespace Gibs.Api.Controllers
{
    [Route("api/settings")]
    public class SettingsController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet]
        public async Task<IEnumerable<SettingResponse>> ListSettings()
        {
            var paging = new PaginationQuery(1000);
            var query = UoW.Settings.AsNoTracking();

           return await PagedList(query, paging, setting => new SettingResponse(setting));
        }

        [HttpPost]
        public async Task<IEnumerable<SettingResponse>> UpdateSettings(UpdateSettingRequest[] dto)
        {
            foreach (var request in dto)
            {
                var setting = await UoW.Settings.FindAsync(request.SettingID)
                   ?? throw new NotFoundException($"Setting [{request.SettingID}] was not found");

                setting.UpdateValue(request.Value);
            }

            await UoW.SaveChangesAsync();
            return await ListSettings();
        }
    }
}
