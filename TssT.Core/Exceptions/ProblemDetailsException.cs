using System;
using Microsoft.AspNetCore.Mvc;

namespace TssT.Core.Exceptions
{
    public abstract class ProblemDetailsException: Exception
    {
        public ProblemDetails Details { get; }
        protected ProblemDetailsException(ProblemDetails details)
        {
            Details = details;
        }
    }
}