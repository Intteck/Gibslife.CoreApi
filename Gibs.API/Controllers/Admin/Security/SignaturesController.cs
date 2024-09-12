using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;
using Gibs.Api.Controllers;
using Gibs.Domain.Entities;
using Gibs.Domain;

namespace Gibs7.Api.Controllers
{
    [Route("api/users/{userId}/signatures")]
    public class SignatureController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet("/media/{signatureId}")]
        public async Task<IActionResult> GetSignature(Guid signatureId)
        {
            var signature = await UoW.Signatures.Include(x => x.Blob)
                                                .SingleOrDefaultAsync(x => x.BlobId == signatureId)
               ?? throw new NotFoundException($"Signature [{signatureId}] was not found");

            //////////////////////////////////
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "TempAssets");
            
            if (!Directory.Exists(pathToSave))
                Directory.CreateDirectory(pathToSave);

            var filePath = Path.Combine(pathToSave, signatureId.ToString());
            //////////////////////////////////

            if (!System.IO.File.Exists(filePath))
            {
                await System.IO.File.WriteAllBytesAsync(filePath, signature.Blob.Data);
            }
            return PhysicalFile(filePath, signature.Blob.ContentType);
        }

        [HttpGet("/api/signatures")]
        public async Task<IEnumerable<SignatureResponse>> ListSignatures(
            [FromQuery] PaginationQuery paging)
        {
            var query = UoW.Signatures.OrderBy(x => x.Id).AsNoTracking();

            return await PagedList(query, paging, 
                s => new SignatureResponse(s));
        }

        [HttpGet]
        public async Task<IEnumerable<SignatureResponse>> ListSignatures(
            [FromRoute] string userId)
        {
            var query = UoW.Signatures.Where(x => x.User.Id == userId)
                                      .OrderBy(x => x.Id)
                                      .AsNoTracking();

            var paging = new PaginationQuery(200);
            return await PagedList(query, paging, 
                s => new SignatureResponse(s));
        }

        [HttpPost]
        [Consumes("multipart/form-data")] 
        public async Task<SignatureResponse> AddSignature(
            [FromRoute] string userId,
            [FromForm] string description,
            /*[FromForm]*/ IFormFile file)
        {
            var user = await UoW.Users.FindAsync(userId)
               ?? throw new NotFoundException($"User [{userId}] was not found");

            if (file == null || file.Length == 0)
                throw new InvalidOperationException("Please select a file to upload");

            if (string.IsNullOrEmpty(file.ContentType))
                throw new InvalidOperationException("The upload must have a ContentType value");

            var bytes = file.OpenReadStream().ToBytes();
            var blob = new Blob(bytes, file.ContentType, $"Signature for {user.Id} - {description}");
            var signature = new Signature(description ?? file.FileName, blob);

            user.Signatures.Add(signature);
            //also blob must be added manually?
            //UoW.Entry(blob).State = EntityState.Added;
            UoW.Add(blob);

            await UoW.SaveChangesAsync();
            return new SignatureResponse(signature);
        }

        [HttpDelete("{signatureId}")]
        public async Task DeleteSignature(
            //[FromRoute] string userId,
            [FromRoute] Guid signatureId)
        {
            //var user = await UoW.Users.FindAsync(userId)
            //   ?? throw new NotFoundException($"User [{userId}] was not found");

            var signature = await UoW.Signatures.Include(x => x.Blob)
                                                .SingleOrDefaultAsync(x => x.BlobId == signatureId)
               ?? throw new NotFoundException($"Signature [{signatureId}] was not found");

            //also cascade delete blob media
            //Blob is the Principal in the relationship because it has a FK on Signatures table
            UoW.Remove(signature.Blob); //this alone cascade deletes both
            //UoW.Remove(signature); //this alone deletes signature only
            await UoW.SaveChangesAsync();
        }
    }
}
