using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Extensions;

public static class StringExtensions
{
    public static string CreateMd5(this string key)
    {
        var sb = new StringBuilder();
        using var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(key);
        var hashBytes = md5.ComputeHash(inputBytes);

        foreach (var t in hashBytes)
        {
            sb.Append(t.ToString("X2"));
        }
        return sb.ToString();
    }

    public static T ConvertToObject<T>(this string data) where T : class
    {
        return JsonConvert.DeserializeObject<T>(data);
    }

    public static string ConvertToString<T>(this T obj) where T : class
    {
        var dataObject = obj == null ? null : JsonConvert.SerializeObject(obj);
        return dataObject == "[]" ? null : dataObject;
    }
}