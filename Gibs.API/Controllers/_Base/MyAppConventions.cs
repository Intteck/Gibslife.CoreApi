using Gibs.Api.Contracts.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Gibs.Api.Controllers
{
    public static class MyAppConventions
    {
        [ProducesResponseType(200)]
        [ProducesDefaultResponseType(typeof(ApiErrorResponse))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        public static void Default(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] params object[] p) 
        { 
            //this matches ALL endpoints!
        }
    }

    //[ProducesResponseType<ApiErrorResponse>(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType<ApiErrorResponse>(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType<ApiErrorResponse>(StatusCodes.Status404NotFound)]
    //[ProducesResponseType<ApiErrorResponse>(StatusCodes.Status500InternalServerError)]
    //[ProducesResponseType<ApiErrorResponse>(StatusCodes.Status501NotImplemented)]
}
