using System;

namespace TssT.Businesslogic.Exceptions
{
    public class EntityNotFoundException: Exception
    {
        public EntityNotFoundException(string? message):base(message)
        {
        }
    }
}