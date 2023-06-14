using System.Text;

namespace Mimirorg.Common.Models;

public class DatabaseConfiguration
{
    public string DataSource { get; set; } = "127.0.0.1";
    public int Port { get; set; } = 1433;
    public string InitialCatalog { get; set; }
    public string DbUser { get; set; }
    public string Password { get; set; }

    public string ConnectionString => CreateConnectionString();

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        sb.AppendLine("########################## DatabaseConfiguration ############################");
        sb.AppendLine("DataSource:              " + DataSource);
        sb.AppendLine("Port:                    " + Port);
        sb.AppendLine("InitialCatalog:          " + InitialCatalog);
        sb.AppendLine("DbUser:                  " + DbUser);
        sb.AppendLine("#############################################################################");

        return sb.ToString();
    }

    #region Private methods

    private string CreateConnectionString()
    {
        if (string.IsNullOrWhiteSpace(DataSource) || string.IsNullOrWhiteSpace(InitialCatalog) ||
            string.IsNullOrWhiteSpace(DbUser) || string.IsNullOrWhiteSpace(Password))
            return null;


        var portString = string.Empty;
        if (Port > 0) portString = $",{Port}";

        return $@"Data Source={DataSource}{portString};Initial Catalog={InitialCatalog};Integrated Security=False;User ID={DbUser};Password='{Password}';TrustServerCertificate=True;MultipleActiveResultSets=True";
    }

    #endregion
}