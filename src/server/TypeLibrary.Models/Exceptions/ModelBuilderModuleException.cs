using System;

namespace TypeLibrary.Models.Exceptions
{
    [Serializable]
    public class ModelBuilderModuleException : Exception
    {
        public ModelBuilderModuleException(string message) : base(message)
        {

        }
    }
}
