using System.Text;

namespace Mimirorg.Authentication.Models;

public class MimirorgAuthSettings
{
    public string ApplicationName { get; set; }
    public string JwtKey { get; set; }
    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
    public int QrWidth { get; set; } = 300;
    public int QrHeight { get; set; } = 300;
    public int JwtExpireMinutes { get; set; } = 15;
    public int JwtRefreshExpireMinutes { get; set; } = 1440;
    public int MaxFailedAccessAttempts { get; set; } = 5;
    public int DefaultLockoutMinutes { get; set; } = 1440;
    public bool RequireConfirmedAccount { get; set; } = true;
    public bool RequireDigit { get; set; } = false;
    public bool RequireUppercase { get; set; } = false;
    public bool RequireNonAlphanumeric { get; set; } = false;
    public int RequiredLength { get; set; } = 10;
    public string Email { get; set; }
    public string EmailKey { get; set; }
    public string EmailSecret { get; set; }
    public string EmailHost { get; set; } = "localhost";
    public int EmailPort { get; set; } = 25;

    public DatabaseConfiguration DatabaseConfiguration { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        sb.AppendLine("############################ Auth settings ####################################################");
        sb.AppendLine($"{nameof(ApplicationName)}:          {ApplicationName}");
        sb.AppendLine($"{nameof(JwtExpireMinutes)}:         {JwtExpireMinutes}");
        sb.AppendLine($"{nameof(JwtRefreshExpireMinutes)}:  {JwtRefreshExpireMinutes}");
        sb.AppendLine($"{nameof(MaxFailedAccessAttempts)}:  {MaxFailedAccessAttempts}");
        sb.AppendLine("-----------------------------------------------------------------------------------------------");
        sb.AppendLine($"{nameof(RequireConfirmedAccount)}:  {RequireConfirmedAccount}");
        sb.AppendLine($"{nameof(RequireDigit)}:             {RequireDigit}");
        sb.AppendLine($"{nameof(RequireUppercase)}:         {RequireUppercase}");
        sb.AppendLine($"{nameof(RequireNonAlphanumeric)}:   {RequireNonAlphanumeric}");
        sb.AppendLine($"{nameof(RequiredLength)}:           {RequiredLength}");
        sb.AppendLine("-----------------------------------------------------------------------------------------------");
        sb.AppendLine($"{nameof(Email)}:                    {Email}");
        sb.AppendLine($"{nameof(EmailKey)}:                 {EmailKey}");
        sb.AppendLine($"{nameof(EmailHost)}:                {EmailHost}");
        sb.AppendLine($"{nameof(EmailPort)}:                {EmailPort}");
        sb.AppendLine("-----------------------------------------------------------------------------------------------");
        if (DatabaseConfiguration != null)
        {
            sb.AppendLine($"{nameof(DatabaseConfiguration.DataSource)}:               {DatabaseConfiguration.DataSource}");
            sb.AppendLine($"{nameof(DatabaseConfiguration.Port)}:                     {DatabaseConfiguration.Port}");
            sb.AppendLine($"{nameof(DatabaseConfiguration.InitialCatalog)}:           {DatabaseConfiguration.InitialCatalog}");
            sb.AppendLine($"{nameof(DatabaseConfiguration.DbUser)}:                   {DatabaseConfiguration.DbUser}");
        }

        sb.AppendLine("############################ Auth settings ####################################################");
        return sb.ToString();
    }
}