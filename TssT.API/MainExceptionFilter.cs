using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TssT.Core.Errors;

namespace TssT.API
{
    public class MainExceptionFilter: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            switch (context.Exception)
            {
                case ValidationException exception:
                    context.Result = new ObjectResult(new
                    {
                        exception.Message
                    }) {StatusCode = 400};
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }
}