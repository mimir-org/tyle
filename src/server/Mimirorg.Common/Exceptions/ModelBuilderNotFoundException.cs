namespace Mimirorg.Common.Exceptions
{
    [Serializable]
    public class ModelBuilderNotFoundException : Exception
    {
        public ModelBuilderNotFoundException(string message) : base(message)
        {

        }
    }
}
