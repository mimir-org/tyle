namespace Mimirorg.Common.Exceptions;

[Serializable]
public class MimirorgConfigurationException : Exception
{
    public MimirorgConfigurationException(string message) : base(message)
    {

    }
}