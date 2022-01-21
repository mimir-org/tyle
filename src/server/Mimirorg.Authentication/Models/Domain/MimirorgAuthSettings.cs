namespace Mimirorg.Authentication.Models.Domain
{
    public class MimirorgAuthSettings
    {
        public string ApplicationName { get; set; }
        public int QrWidth { get; set; }
        public int QrHeight { get; set; }
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public int JwtExpireMinutes { get; set; }
        public int JwtRefreshExpireMinutes { get; set; }
    }
}
