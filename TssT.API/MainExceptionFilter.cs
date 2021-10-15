using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TssT.Businesslogic.Exceptions;

namespace TssT.API
{
    public class MainExceptionFilter: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not null)
            {
                var problemDetails = new ProblemDetails()
                {
                    Type = "https://example.com/unhandled",
                    Detail = context.Exception.Message,
                    Title = "Неизвестная ошибка",
                    Status = 500
                };
                
                switch (context.Exception)
                {
                    case ValidationException validationException:
                        problemDetails = new ProblemDetails
                        {
                            Type = "https://example.com/validation",
                            Detail = validationException.Message,
                            Title = "Ошибка валидации",
                            Status = 400
                        };
                        break;
                
                    case EntityNotFoundException entityNotFoundException:
                        problemDetails = new ProblemDetails
                        {
                            Type = "https://example.com/entity-not-found",
                            Detail = entityNotFoundException.Message,
                            Title = "Объект не найден",
                            Status = 400
                        };
                        break;
                }
                
                context.Result = new ObjectResult(problemDetails)
                {
                    ContentTypes = {"application/problem+json"},
                    StatusCode = problemDetails.Status
                };

                context.ExceptionHandled = true;
            }
        }
    }
}