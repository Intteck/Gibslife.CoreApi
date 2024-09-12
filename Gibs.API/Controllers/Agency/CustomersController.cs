using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;

namespace Gibs.Api.Controllers
{
    [Route("api/customers")]
    public class CustomersController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet]
        public async Task<IEnumerable<CustomerResponse>> ListCustomers(
            [FromQuery] PaginationQuery paging)
        {
            var query = UoW.Customers.OrderBy(x => x.FullName).AsNoTracking();

            return await PagedList(query, paging, customer => new CustomerResponse(customer));
        }

        [HttpGet("{customerId}")]
        public async Task<CustomerResponse> GetCustomer(string customerId)
        {
            var customer = await UoW.Customers.FindAsync(customerId) 
                ?? throw new NotFoundException($"Customer [{customerId}] was not found");
           
            return new CustomerResponse(customer);
        }

        [HttpPost]
        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest dto)
        {
            var customer = await GetOrAddCustomer(dto);

            await UoW.SaveChangesAsync();

            //var uri = new Uri($"{Request.Path}/{customer.CustomerID}", UriKind.Relative);
            //return Created(uri, new CustomerResponse(customer));
            return new CustomerResponse(customer);
        }

        [HttpPost("{customerId}")]
        public async Task<CustomerResponse> UpdateCustomer(string customerId, UpdateCustomerRequest dto)
        {
            var customer = await UoW.Customers.FindAsync(customerId)
               ?? throw new NotFoundException($"Customer [{customerId}] was not found");

            customer.UpdateContact(dto.Street, dto.CityLGA, dto.StateID, dto.Phone, dto.PhoneAlt);
            customer.UpdatePersonal(dto.CountryID, dto.BirthDate, "");
            customer.UpdateKyc(dto.TaxNumber, dto.KycTypeID, dto.KycNumber, dto.KycIssueDate, dto.KycExpiryDate);
            await UoW.SaveChangesAsync();

            return new CustomerResponse(customer);
        }

        [HttpDelete("{customerId}")]
        public async Task DeleteCustomer(string customerId)
        {
            var customer = await UoW.Customers.FindAsync(customerId)
               ?? throw new NotFoundException($"Customer [{customerId}] was not found");

            //UoW.Remove(customer);
            await UoW.SaveChangesAsync();
        }
    }
}
