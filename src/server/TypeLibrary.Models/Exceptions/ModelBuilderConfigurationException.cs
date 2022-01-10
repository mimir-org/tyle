using System;

namespace TypeLibrary.Models.Exceptions
{
    [Serializable]
    public class ModelBuilderConfigurationException : Exception
    {
        public ModelBuilderConfigurationException(string message) : base(message)
        {

        }
    }
}
