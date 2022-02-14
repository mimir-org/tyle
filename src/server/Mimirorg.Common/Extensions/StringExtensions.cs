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

        public static string ResolveNormalizedName(this string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            var withOutSpecialCharacters = new string(name.Where(c => char.IsLetterOrDigit(c) && !(char.IsWhiteSpace(c) || c is '-')).ToArray());
            return withOutSpecialCharacters.ToUpper();
        }

        public static string IncrementMajorVersion(this string version)
        {
            return IncrementVersion(version, true, false, false);
        }

        public static string IncrementMinorVersion(this string version)
        {
            return IncrementVersion(version, false, true, false);
        }

        public static string IncrementPatchVersion(this string version)
        {
            return IncrementVersion(version, false, false, true);
        }

        #region Private

        private static string IncrementVersion(string version, bool incrementMajor, bool incrementMinor, bool incrementPatch)
        {
            const int incrementStep = 1;

            if (string.IsNullOrWhiteSpace(version))
                return version;

            var versionStringSplit = version.Trim().Split(".");

            if (versionStringSplit.Length is < 2 or > 3)
                return version;

            string newVersion;
            int versionNumber;

            if (incrementMajor)
            {
                versionNumber = Convert.ToInt32(versionStringSplit[0]) + incrementStep;
                newVersion = versionNumber + ".0";
                return versionStringSplit.Length == 2 ? newVersion : newVersion + ".0";
            }

            if (incrementMinor)
            {
                versionNumber = Convert.ToInt32(versionStringSplit[1]) + incrementStep;
                newVersion = versionStringSplit[0] + "." + versionNumber;
                return versionStringSplit.Length == 2 ? newVersion : newVersion + "." + versionStringSplit[2];
            }

            if (!incrementPatch || versionStringSplit.Length != 3)
                return version;

            versionNumber = Convert.ToInt32(versionStringSplit[2]) + incrementStep;
            newVersion = versionStringSplit[0] + "." + versionStringSplit[1] + "." + versionNumber;

            return newVersion;
        }

        #endregion Private
    }
}
