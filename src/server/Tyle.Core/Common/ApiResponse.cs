namespace Tyle.Core.Common;

public class ApiResponse<T>
{
    public List<string> ErrorMessage { get; set; } = new();
    public T? TValue { get; set; }

}