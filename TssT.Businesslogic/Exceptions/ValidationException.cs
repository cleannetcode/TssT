using System;

namespace TssT.Businesslogic.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string? message) : base(message)
        {
        }
    }
}