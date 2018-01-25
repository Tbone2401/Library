using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Library.AzureStorage
{
    public class ImageService
    {
        public async Task<string> ProcessImage(HttpPostedFileBase imageToUpload)
        {
            string imageName = "";
            imageName = string.Format(Guid.NewGuid().ToString() + '.' + GetImgExtension(imageToUpload));

            return await UploadImageAsync(imageToUpload, imageName);
        }
        public async Task<string> UpdateImage(HttpPostedFileBase imageToUpload, string photoName)
        {
            return await UploadImageAsync(imageToUpload, photoName);
        }

        public string GetImgExtension(HttpPostedFileBase imageToUpload)
        {
            return imageToUpload.ContentType.Substring(imageToUpload.ContentType.IndexOf('/')+1);
        }

        public async Task<string> UploadImageAsync(HttpPostedFileBase imageToUpload, string photoName)
        {
            if (CheckIfValid(imageToUpload)) return null;

            var cloudBlobContainer = SetupCloudBlobContainer();

            await ConfigureBlobContainer(cloudBlobContainer);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(photoName);
            cloudBlockBlob.Properties.ContentType = imageToUpload.ContentType;
            await cloudBlockBlob.UploadFromStreamAsync(imageToUpload.InputStream);
            return photoName;
        }

        private static bool CheckIfValid(HttpPostedFileBase imageToUpload)
        {
            if (imageToUpload == null || imageToUpload.ContentLength == 0)
            {
                return true;
            }
            return false;
        }

        private static CloudBlobContainer SetupCloudBlobContainer()
        {
            CloudStorageAccount cloudStorageAccount = ConnectionString.GetConnectionString();
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(
                string.Format(ConfigurationManager.AppSettings["PictureStorageUrl"]));
            return cloudBlobContainer;
        }

        private static async Task ConfigureBlobContainer(CloudBlobContainer cloudBlobContainer)
        {
            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(
                    new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    }
                );
            }
        }
    }
}