namespace Tyle.Core.Exceptions;

[Serializable]
public class MimirorgBadRequestException : Exception
{
    public MimirorgBadRequestException(string message) : base(message)
    {
    }
}
