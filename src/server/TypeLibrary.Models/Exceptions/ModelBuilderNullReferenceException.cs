using System;

namespace TypeLibrary.Models.Exceptions
{
    [Serializable]
    public class ModelBuilderNullReferenceException : Exception
    {
        public ModelBuilderNullReferenceException(string message) : base(message)
        {

        }
    }
}
