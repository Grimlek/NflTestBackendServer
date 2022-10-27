using AngularTestBackendServer.Core;
using AngularTestBackendServer.Core.Models.Indexes;
using Azure;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace AngularTestBackendServer.Data.Initializers;

public static class AzureIndexerInitializer
{
    /// <summary>
    /// Creates indexers for Seasons to a single index
    /// Exists because indexers for the free tier only allow up to 10000 documents
    /// </summary>
    /// <param name="dataSourceSettings"></param>
    public static void InitializeNflSeasonIndexers(AppSettings.DataSourceSettings dataSourceSettings)
    {
        var azureSearchSettings = dataSourceSettings.AzureSearch;
        var azureSearchUri = new Uri(azureSearchSettings.Endpoint);
        var azureKeyCredentials = new AzureKeyCredential(azureSearchSettings.ApiKey);
        
        var indexerClient = new SearchIndexerClient(azureSearchUri, azureKeyCredentials);
        
        var fieldBuilder = new FieldBuilder();
        var searchFields = fieldBuilder.Build(typeof(SeasonIndex));

        var searchIndex = new SearchIndex(azureSearchSettings.SeasonsIndexName, searchFields);
        
        var dataSourceIndexer1 =
                new SearchIndexerDataSourceConnection(
                    $"{azureSearchSettings.SeasonsIndexerName1}-datasource",
                    SearchIndexerDataSourceType.AzureSql,
                    dataSourceSettings.NflConnectionString,
                    new SearchIndexerDataContainer(azureSearchSettings.SeasonsIndexerSqlView1));
                                                                                             
        var dataSourceIndexer2 =                                                                             
                new SearchIndexerDataSourceConnection(
                    $"{azureSearchSettings.SeasonsIndexerName2}-datasource",                                              
                    SearchIndexerDataSourceType.AzureSql,                                            
                    dataSourceSettings.NflConnectionString,                                          
                    new SearchIndexerDataContainer(azureSearchSettings.SeasonsIndexerSqlView2));     
        
        // The data source does not need to be deleted if it was already created,
        // but the connection string may need to be updated if it was changed
        indexerClient.CreateOrUpdateDataSourceConnection(dataSourceIndexer1);
        indexerClient.CreateOrUpdateDataSourceConnection(dataSourceIndexer2);
        
        var indexer1 = 
            new SearchIndexer(azureSearchSettings.SeasonsIndexerName1, dataSourceIndexer1.Name, searchIndex.Name);
        
        indexerClient.CreateOrUpdateIndexer(indexer1);
        indexerClient.RunIndexer(indexer1.Name);
        
        var indexer2 = 
            new SearchIndexer(azureSearchSettings.SeasonsIndexerName2, dataSourceIndexer2.Name, searchIndex.Name);
        
        indexerClient.CreateOrUpdateIndexer(indexer2);
        indexerClient.RunIndexer(indexer2.Name);
    }
}