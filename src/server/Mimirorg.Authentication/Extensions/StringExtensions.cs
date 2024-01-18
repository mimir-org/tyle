using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Mimirorg.Authentication.Extensions;

public static class StringExtensions
{
    public static string ConvertToString<T>(this T obj) where T : class
    {
        var dataObject = obj == null ? null : JsonConvert.SerializeObject(obj);
        return dataObject == "[]" ? null : dataObject;
    }

    public static string CreateSha512(this string key)
    {
        var sb = new StringBuilder();
        using var sha = SHA512.Create();
        var inputBytes = Encoding.ASCII.GetBytes(key);
        var hashBytes = sha.ComputeHash(inputBytes);

        foreach (var t in hashBytes)
        {
            sb.Append(t.ToString("X2"));
        }
        return sb.ToString();
    }
}