namespace Tyle.Api
{
    public static class CustomExeptionHandler
    {
        public static Exception ExceptionWithLogger(this Exception exception)
        {
            //Do logging here
            return exception;
        }
    }
}