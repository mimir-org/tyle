using System.Security.Cryptography;
using System.Text;
using Mimirorg.Common.Exceptions;

namespace Mimirorg.Common.Extensions
{
    public static class StringExtensions
    {
        public static ICollection<string> ConvertToArray(this string value)
        {
            return string.IsNullOrEmpty(value) ?
                new List<string>() :
                value.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        }

        public static string ConvertToString(this ICollection<string> values)
        {
            if (!values.Any())
                return null;

            var returnValue = values.Aggregate(string.Empty, (current, value) => current + $"{value},");
            return returnValue.TrimEnd(',');
        }

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

        public static string ResolveNameFromRoleClaim(this string role)
        {
            if (string.IsNullOrEmpty(role))
                return string.Empty;

            var name = role.Split('_', StringSplitOptions.RemoveEmptyEntries);
            if (name.Length != 2)
                throw new MimirorgInvalidOperationException("The role name contains fail format.");

            return name[^1];
        }

        public static string ResolveDomain(this string id)
        {
            var idSplit = id?.Split('_', StringSplitOptions.RemoveEmptyEntries);
            return idSplit?.Length != 2 ? null : idSplit[0];
        }
    }
}
