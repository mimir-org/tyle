namespace Tyle.Core.Common.Exceptions;

[Serializable]
public class MimirorgNotFoundException : Exception
{
    public MimirorgNotFoundException(string message) : base(message)
    {

    }
}