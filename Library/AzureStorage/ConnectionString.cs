using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Library.AzureStorage
{
    public static class ConnectionString
    {
        static string account = ConfigurationManager.AppSettings["StorageAccountName"];
        static string key = ConfigurationManager.AppSettings["StorageAccountKey"];

        public static CloudStorageAccount GetConnectionString()
        {
            string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", account, key);
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}