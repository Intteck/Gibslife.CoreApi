using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gibs.Api.Contracts.V1
{
    internal class ApiErrorResult : ObjectResult
    {
        public ApiErrorResult(int code, string? message) : base(null)
        {
            StatusCode = code;
            Value = new ApiErrorResponse(code, message);
        }

        public ApiErrorResult(ModelStateDictionary modelState) : base(null)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            Value = new ApiErrorResponse(modelState);
        }
    }

    internal class ApiErrorResponse
    {
        public ApiErrorResponse(int code, string? message)
        {
            Code = code;
            Message = message;
        }

        public ApiErrorResponse(ModelStateDictionary modelState)
        {
            Code = 111;
            Message = "Validation Failed";
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key]!.Errors
                    .Select(x => new Error(key, x.ErrorMessage)))
                    .ToArray();
        }

        public int Code { get; set; }
        public string? Message { get; set; } 
        public Error[]? Errors { get; set; }  

        public record class Error(string Field, string Message);
    }
}
