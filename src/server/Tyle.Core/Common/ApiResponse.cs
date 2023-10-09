namespace Tyle.Core.Common;

public class ApiResponse<T>
{
    public ApiResponse()
    {
        ErrorMessage = new List<string>();
    }

    private List<string>? _errorMessage;
    public List<string> ErrorMessage
    {
        get { return _errorMessage; }
        set
        {
            _errorMessage = value;
            HasError = _errorMessage.Any();
            if (HasError)
            {
                LogError(_errorMessage.Last(), new System.Diagnostics.StackTrace());
            }
        }
    }
    public bool HasError { get; private set; }

    public T? TValue { get; set; }

    private void LogError(string errorMessage, System.Diagnostics.StackTrace stackTrace)
    {
        // Replace this with your actual logging code.
        Console.WriteLine($"Error logged: {errorMessage}");
        Console.WriteLine($"Stack trace:\n{stackTrace}");
    }
}