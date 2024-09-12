using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Gibs.Api.Contracts.V1;

namespace Gibs.Api.Controllers
{
    internal class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ArgumentException ex: // Argument[Null|OutOfRange]Exception
                    context.ModelState.AddModelError(ex.ParamName ?? "unknown", ex.Message);
                    context.Result = new ApiErrorResult(context.ModelState);
                    return;

                case NotFoundException ex:
                    context.Result = new ApiErrorResult(StatusCodes.Status404NotFound, ex.Message);
                    return;

                case UnauthorizedException ex:
                    context.Result = new ApiErrorResult(StatusCodes.Status401Unauthorized, ex.Message);
                    return;

                case NotImplementedException ex:
                    context.Result = new ApiErrorResult(StatusCodes.Status501NotImplemented, ex.Message);
                    return;

                case InvalidOperationException ex:
                    context.Result = new ApiErrorResult(StatusCodes.Status400BadRequest, ex.Message);
                    return;

                case DbUpdateException ex:
                    context.Result = new ApiErrorResult(StatusCodes.Status406NotAcceptable, ex.GetErrorMessage());
                    return;

                default:
                    var ex1 = context.Exception;
                    context.Result = new ApiErrorResult(StatusCodes.Status500InternalServerError, ex1.Message);
                    return;
            }
        }
    }

    public static class Extension
    {
        public static string GetErrorMessage(this DbUpdateException ex)
        {
            if (ex.InnerException != null && ex.InnerException.Message.Contains("PRIMARY KEY"))
                return "This item already exists in the Database";

            return ex.InnerException?.Message ?? ex.Message;
        }
    }

    public class NotFoundException(string message) : Exception(message) { }
    public class UnauthorizedException(string message) : Exception(message) { }
}