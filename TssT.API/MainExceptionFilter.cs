using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TssT.Core.Exceptions;

namespace TssT.API
{
    public class MainExceptionFilter: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            switch (context.Exception)
            {
                case ProblemDetailsException exception:
                    context.Result = new ObjectResult(exception.Details)
                    {
                        ContentTypes = { "application/problem+json" },
                        StatusCode = exception.Details.Status
                    };
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }
}