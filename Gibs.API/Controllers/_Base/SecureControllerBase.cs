using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;
using Gibs.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Gibs.Api.Controllers
{
    //#if !DEBUG
    //    [Microsoft.AspNetCore.Authorization.Authorize]
    //#endif
    //    [Microsoft.AspNetCore.Authorization.Authorize]
    [ApiController]
    [ApiExceptionFilter]
    [Produces("application/json")]
    [ApiConventionType(typeof(MyAppConventions))]
    public abstract class SecureControllerBase : ControllerBase
    {
        protected ControllerServices Services { get; }
        protected GibsContext UoW { get; }

        protected SecureControllerBase(ControllerServices services)
        {
            Services = services;
            UoW = services.DbContext;

            //set the GetLogonUser() delegate
            UoW.GetLogonUser = () => LoginUser(services.HttpContext);
        }

        private static User LoginUser(HttpContext? httpContext)
        {
#if DEBUG
            return new User("admin", "pass", "Christiana", "Ronaldo", "08055555247", "demo@email.co");
#endif

            if (httpContext == null)
                throw new Exception("Services.HttpContext is NULL");

            //var principal = httpContext.User ?? throw new Exception("Invalid LoginUser - principal");
            //var claim = principal.FindFirst(ClaimTypes.Email) ?? throw new Exception("Invalid LoginUser - claim");
            //return new User(claim.Value, "pass", claim.Value, claim.Value, "08055555247", "demo@email.co");

            return new User("ict", "pass", "FirstName", "LastName", "08055555247", "demo@email.co");
        }

        protected async Task<Customer> GetOrAddCustomer(CreateCustomerRequest c)
        {
            if (c.BirthDate == null)
                throw new ArgumentNullException(nameof(c.BirthDate), "BirthDate cannot be null");

            DateOnly dob = c.BirthDate.Value;

            var customer = await UoW.Customers
                                    .AsNoTracking()
                                    .Where(x => x.LastName == c.LastName
                                             && x.BirthDate == dob)
                                    .FirstOrDefaultAsync();
            if (customer is null)
            {
                var codeFactory = UoW.GetCodeFactory(ApiDefaults.HqBranch, null);

                customer = new Customer(codeFactory, false, c.Title, c.LastName, c.FirstName, c.OtherNames, c.Email, c.Street);

                customer.UpdateContact(c.Street, c.CityLGA, c.StateID, c.Phone, c.PhoneAlt);
                customer.UpdatePersonal(c.CountryID, c.BirthDate, "");
                customer.UpdateKyc(c.TaxNumber, c.KycTypeID, c.KycNumber, c.KycIssueDate, c.KycExpiryDate);

                UoW.Customers.Add(customer);
            }
            return customer;
        }

        protected async Task<IEnumerable<TDestination>> PagedList<TSource, TDestination>(
            IQueryable<TSource> query, PaginationQuery paging,
            Func<TSource, TDestination> converter)
        {
            var list = await query.ToPagedListAsync(paging);

            Response.Headers[Settings.Headers.PAGE_SIZE] = list.PageSize.ToString();
            Response.Headers[Settings.Headers.PAGE_NUMBER] = list.PageNumber.ToString();
            Response.Headers[Settings.Headers.TOTAL_PAGES] = list.TotalPages.ToString();
            Response.Headers[Settings.Headers.TOTAL_ITEMS] = list.TotalItems.ToString();

            return list.Select(converter);
        }

        protected async Task<ActionResult<IEnumerable<TDestination>>> PagedResult<TSource, TDestination>(
            IQueryable<TSource> query, PaginationQuery paging,
            Func<TSource, TDestination> converter)
        {
            var list = await PagedList(query, paging, converter);
            return new OkObjectResult(list);
        }

        public static class ApiDefaults
        {
            public static FxCurrency LocalCurrency { get; } = FxCurrency.FactoryCreate("NGN", "Naira", "N", 1);

            public static Branch Branch { get; } = new("EB", "E BUSINESS", "500", "01", "001");
            public static Branch HqBranch { get; } = new("LAG", "API-HQ", "01", "25", "001");
            public static string AgentID { get; } = "DI-000009";
            public static string MarketerID { get; } = "SG-0999";   // OTHERS
            public static string ChannelID { get; } = "31";         // E-BUSINESS
            public static string SubChannelID { get; } = "100";     // E-CHANNEL DIRECT
            public static string ControlAccountID { get; } = "1112000000";
        }
    }
}