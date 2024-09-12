using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Gibs.Api.Contracts.V1;
using Gibs.Api.Controllers;
using Gibs.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gibs7.Api.Controllers
{
    [Route("api/products")]

    public class ProductsController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet("{productId}")]
        public async Task<ProductResponse> GetProduct(string productId)
        {
            var product = await UoW.Products.Include(x => x.Sections)
                                            .ThenInclude(x => x.SMIs)
                                            .Where(x => x.Id == productId)
                                            .SingleOrDefaultAsync()
                ?? throw new NotFoundException($"Product [{productId}] was not found");

            return new ProductResponse(product);
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResponse>> ListProducts()
        {
            var query = UoW.Products.Include(x => x.Sections)
                                    .ThenInclude(x => x.SMIs)
                                    .OrderBy(x => x.Name)
                                    .AsNoTracking();

            var paging = new PaginationQuery(200);
            return await PagedList(query, paging, product => new ProductResponse(product));
        }

        [HttpPost]
        public async Task<ProductResponse> CreateProduct(CreateProductRequest dto)
        {
            var risk = await UoW.Classes.FindAsync(dto.RiskID)
                ?? throw new NotFoundException($"Risk [{dto.RiskID}] was not found");

            //var midRisk = await risk.MidClasses
            //                        .Where(x => x.MidClassID == dto.MidClassID)
            //                        .SingleOrDefaultAsync();

            var product = new Product(dto.ProductID, null, risk, dto.ProductName, dto.AlternateName, dto.NaicomID);
            UoW.Products.Add(product);

            await UoW.SaveChangesAsync();

            return new ProductResponse(product);
        }

        [HttpPost("{productId}")]
        public async Task<ProductResponse> UpdateProduct(string productId, UpdateProductRequest dto)
        {
            var product = await UoW.Products.FindAsync(productId)
               ?? throw new NotFoundException($"Product [{productId}] was not found");

            //product.UpdateProduct(dto.ProductName);
            await UoW.SaveChangesAsync();

            return new ProductResponse(product);
        }

        [HttpDelete("{productId}")]
        public async Task DeleteProduct(string productId)
        {
            var product = await UoW.Products.FindAsync(productId)
               ?? throw new NotFoundException($"Product [{productId}] was not found");

            UoW.Remove(product);
            await UoW.SaveChangesAsync();
        }
    }
}
