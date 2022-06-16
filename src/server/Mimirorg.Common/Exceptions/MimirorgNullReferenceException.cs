namespace Mimirorg.Common.Exceptions
{
    [Serializable]
    public class MimirorgNullReferenceException : Exception
    {
        public MimirorgNullReferenceException(string message) : base(message)
        {

        }
    }
}