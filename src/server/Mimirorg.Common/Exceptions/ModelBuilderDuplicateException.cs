namespace Mimirorg.Common.Exceptions
{
    [Serializable]
    public class ModelBuilderDuplicateException : Exception
    {
        public ModelBuilderDuplicateException(string message) : base(message)
        {

        }
    }
}
