namespace AngularTestBackendServer.Core;

public class AppSettings
{
    public DataSourceSettings DataSources { get; set; }

    public class DataSourceSettings
    {
        public string NflConnectionString { get; set; }
        public bool ShouldLoadCsvDataForDatabase { get; set; }
        public AzureSearchSettings AzureSearch { get; set; }
    }

    public class AzureSearchSettings
    {
        public string ApiKey { get; set; }
        public string Endpoint { get; set; }
        public string TeamsIndexName { get; set; }
        public string SeasonsIndexName { get; set; }
        public string SeasonsIndexerName1 { get; set; }
        public string SeasonsIndexerName2 { get; set; }
        public string SeasonsIndexerSqlView1 { get; set; }
        public string SeasonsIndexerSqlView2 { get; set; }
        public bool ShouldCreateMultipleSeasonsIndexer { get; set; }
    }
}