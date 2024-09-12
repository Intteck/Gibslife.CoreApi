using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;
using Gibs.Domain;

namespace Gibs.Api.Controllers
{
    [Route("api/templates")]
    public class TemplatesController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet]
        public async Task<IEnumerable<TemplateResponse>> ListTemplates(
            [FromQuery] PaginationQuery paging)
        {
            var query = UoW.Templates.OrderBy(x => x.Id).AsNoTracking();
            return await PagedList(query, paging, doc => new TemplateResponse(doc));
        }

        [HttpPost("/policy")]
        [Consumes("multipart/form-data")]
        public Task<TemplateResponse> UpdateTemplate(
            [FromForm] string productId,
            /*[FromForm]*/ IFormFile file)
        {
            var id = TemplateID.Create(productId);
            return UpdateTemplate(id, file);
        }

        [HttpPost("/endorsement")]
        [Consumes("multipart/form-data")]
        public Task<TemplateResponse> UpdateTemplate(
            [FromForm] string endorseId,
            [FromForm] string riskId,
            [FromForm] bool isCollective,
            /*[FromForm]*/ IFormFile file)
        {
            var id = TemplateID.Create(endorseId, riskId, isCollective);
            return UpdateTemplate(id, file);
        }

        [HttpGet("{templateId}")]
        public async Task<IActionResult> DownloadTemplate(
            [FromRoute] string templateId)
        {
            var doc = await UoW.Templates.Include(x => x.Blob)
                                         .Where(x => x.Id == templateId)
                                         .SingleOrDefaultAsync()
               ?? throw new NotFoundException($"Document [{templateId}] was not found");

            var filename = $"{doc.Id}_template.docx".ToLower();
            return File(doc.Blob.Data, "application/octet-stream", filename);
        }

        [HttpDelete("{templateId}")]
        public async Task DeleteTemplate(
            [FromRoute] string templateId)
        {
            var doc = await UoW.Templates.Include(x => x.Blob)
                                         .Where(x => x.Id == templateId)
                                         .SingleOrDefaultAsync()
               ?? throw new NotFoundException($"Document [{templateId}] was not found");

            UoW.Remove(doc.Blob);
            UoW.Remove(doc);
            await UoW.SaveChangesAsync();
        }

        private async Task<TemplateResponse> UpdateTemplate(
            TemplateID id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new InvalidOperationException("Please select a file to upload");

            var bytes = file.OpenReadStream().ToBytes();
            var doc = await UoW.Templates.FindAsync(id.TemplateId);

            if (doc == null)
            {
                doc = new TemplateDoc(id, bytes, file.ContentType, file.FileName);
                UoW.Templates.Add(doc);
            }
            else
            {
                doc.UpdateData(bytes, file.ContentType);
            }
            await UoW.SaveChangesAsync();
            return new TemplateResponse(doc);
        }
    }
}

