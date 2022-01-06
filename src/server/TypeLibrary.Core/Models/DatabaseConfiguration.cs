namespace TypeLibrary.Core.Models
{
    public class DatabaseConfiguration
    {
        public string DataSource { get; set; }
        public int Port { get; set; }
        public string InitialCatalog { get; set; }
        public string DbUser { get; set; }
        public string Password { get; set; }
    }
}