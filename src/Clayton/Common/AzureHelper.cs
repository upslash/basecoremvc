using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Microsoft.AspNetCore.Http;

namespace Clayton.Common
{
    public class AzureHelper
    {
        public async Task<string> UploadImage(IFormFile file)
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(
               new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
               "claytonimageupload",
               "YkVbiOUWJleRTttDThcWCbD5x4UZXz8d0itO+A3Fag6lNsoku/sFXmsveLDElcMIoxcT7h+LZLGmCkJojQfocA=="), true);


            // Create a blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to a container named “my-new-container.”
            CloudBlobContainer container = blobClient.GetContainerReference("claytonimages");

            // If “mycontainer” doesn’t exist, create it.
            await container.CreateIfNotExistsAsync();
            // Make available to public
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });


            // Create filename
            var fileName = RandomDigits(10) + file.FileName.Substring(file.FileName.LastIndexOf("."));

            // Get a reference to a blob  
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // Create or overwrite the blob with the contents of a local file 
            using (var fileStream = file.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return blockBlob.Uri.AbsoluteUri;
        }

        private string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
