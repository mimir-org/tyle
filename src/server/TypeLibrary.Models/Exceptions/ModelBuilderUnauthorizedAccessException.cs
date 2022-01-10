using System;

namespace TypeLibrary.Models.Exceptions
{
    public class ModelBuilderUnauthorizedAccessException : Exception
    {
        public ModelBuilderUnauthorizedAccessException(string message) : base(message)
        {

        }
    }
}
