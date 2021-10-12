using System;

namespace TssT.Core.Errors
{
    public class ValidationException : Exception
    {
        public string Description { get; }

        public ValidationException(string? message) : base(message)
        {
            Description = "Ошибка валидации";
        }
    }
}