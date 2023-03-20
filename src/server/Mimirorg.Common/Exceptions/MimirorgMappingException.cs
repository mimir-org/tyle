namespace Mimirorg.Common.Exceptions;

[Serializable]
public class MimirorgMappingException : Exception
{
    public MimirorgMappingException(string message) : base(message)
    {

    }

    public MimirorgMappingException(string from, string to) : base($"Error in mapping from {from} to {to}")
    {

    }
}