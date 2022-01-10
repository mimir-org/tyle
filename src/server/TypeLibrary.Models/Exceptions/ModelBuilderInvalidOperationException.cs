using System;

namespace TypeLibrary.Models.Exceptions
{
    public class ModelBuilderInvalidOperationException : Exception
    {
        public ModelBuilderInvalidOperationException(string message) : base(message)
        {

        }
    }
}
