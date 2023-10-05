namespace Mimirorg.Authentication.Exceptions;

[Serializable]
public class MimirorgNotFoundException : Exception
{
    public MimirorgNotFoundException(string message) : base(message)
    {

    }
}