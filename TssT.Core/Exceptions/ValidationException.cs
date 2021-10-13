using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace TssT.Core.Exceptions
{
    public class ValidationException : ProblemDetailsException
    {
        public ValidationException(string parameter, string? message) : base(new ProblemDetails()
        {
            Type = "https://example.com/validation",
            Instance = null,
            Title = "Некорректные данные",
            Status = 400
        })
        {
            Details.Detail = message;
        }
    }
}