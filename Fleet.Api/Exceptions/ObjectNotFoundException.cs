using System;

namespace Fleet.Api.Exceptions
{
    /// <summary>
    /// Object is not found.
    /// </summary>
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException()
        {
        }

        public ObjectNotFoundException(string message)
            : base(message)
        {
        }

        public ObjectNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
