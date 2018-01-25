using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Library.AzureStorage
{
    public class ImageServiceTest
    {
        public async Task<string> UploadImageAsync(HttpPostedFileBase imageToUpload, string name = "")
        {
            if (imageToUpload == null || imageToUpload.ContentLength == 0)
            {
                return null;
            }

            CloudStorageAccount cloudStorageAccount = ConnectionString.GetConnectionString();
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(ConfigurationManager.AppSettings["PictureFolder"]);

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(
                    new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    }
                );
            }
            if (name == "")
            {
                name = Guid.NewGuid().ToString() + "-" + Path.GetExtension(imageToUpload.FileName);
            }

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(name);
            cloudBlockBlob.Properties.ContentType = imageToUpload.ContentType;
            await cloudBlockBlob.UploadFromStreamAsync(imageToUpload.InputStream);

            return name;
        }
    }
}