using System.Globalization;

// ReSharper disable StringLiteralTypo

namespace Mimirorg.TypeLibrary.Extensions
{
    public static class DateTimeExtensions
    {
        public static string[] DateTimeFormats = new List<string>
        {
            "yyyy-MM-dd",
            "yyyy-MM-ddThh:mm:ss",
            "yyyy-MM-ddThh:mm:ssz",
            "yyyy-MM-ddThh:mm:ss z",
            "yyyy-MM-ddThh:mm:ssZ",
            "yyyy-MM-ddThh:mm:ss Z"
        }.ToArray();

        public static string[] DateTimeOffsetFormats = new List<string>
        {
            "yyyy-MM-ddThh:mm:ss zzz",
            "yyyy-MM-ddThh:mm:sszzz",
            "yyyy-MM-ddThh:mm:ss ZZZ",
            "yyyy-MM-ddThh:mm:ssZZZ"
        }.ToArray();

        public static DateTime? ParseUtcDateTime(this string datetime)
        {
            if (DateTime.TryParseExact(datetime, DateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dt))
            {
                if (dt.Kind != DateTimeKind.Utc)
                    return null;

                return dt;
            }

            if (DateTimeOffset.TryParseExact(datetime, DateTimeOffsetFormats, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dtOffset))
                return dtOffset.UtcDateTime;

            return null;
        }
    }
}
