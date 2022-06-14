using System.Configuration;

namespace Mimirorg.Common.Models
{
    public class DatabaseConfiguration
    {
        public string DataSource { get; set; }
        public int Port { get; set; }
        public string InitialCatalog { get; set; }
        public string DbUser { get; set; }
        public string Password { get; set; }

        public string ConnectionString => CreateConnectionString();

        #region Private methods

        private string CreateConnectionString()
        {
            if (string.IsNullOrWhiteSpace(DataSource) || string.IsNullOrWhiteSpace(InitialCatalog) ||
                string.IsNullOrWhiteSpace(DbUser) || string.IsNullOrWhiteSpace(Password))
                throw new ConfigurationErrorsException("DatabaseConfiguration is missing or invalid");

            var portString = string.Empty;
            if (Port > 0) portString = $",{Port}";


            var connection = $@"Data Source={DataSource}{portString};Initial Catalog={InitialCatalog};Integrated Security=False;User ID={DbUser};Password='{Password}';TrustServerCertificate=True;MultipleActiveResultSets=True";
            Console.WriteLine(connection);

            return connection;
        }

        #endregion
    }
}