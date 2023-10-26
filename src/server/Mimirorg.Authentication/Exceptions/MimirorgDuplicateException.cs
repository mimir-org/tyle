namespace Mimirorg.Authentication.Exceptions;

[Serializable]
public class MimirorgDuplicateException : Exception
{
    public MimirorgDuplicateException(string message) : base(message)
    {

    }
}