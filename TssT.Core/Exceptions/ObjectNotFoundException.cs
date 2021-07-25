using System;

namespace TssT.Core.Exceptions
{
    public class ObjectNotFoundException: Exception
    {
        public ObjectNotFoundException(string message): base(message)
        {
            
        }
        
        public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}