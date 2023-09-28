using Newtonsoft.Json;

namespace Mimirorg.Authentication.Extensions;

public static class StringExtensions
{
    public static string ConvertToString<T>(this T obj) where T : class
    {
        var dataObject = obj == null ? null : JsonConvert.SerializeObject(obj);
        return dataObject == "[]" ? null : dataObject;
    }
}