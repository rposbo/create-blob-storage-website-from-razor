using System.IO;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlobWorker
{
    class BlobUtil
    {
        public BlobUtil(string cloudConnectionString)
        {
            _cloudConnectionString = cloudConnectionString;
        }

        private readonly string _cloudConnectionString;

        public void SaveToLocation(string content, string path, string filename)
        {
            var cloudBlobContainer = GetCloudBlobContainer(path);
            var blob = cloudBlobContainer.GetBlockBlobReference(filename);
            blob.Properties.ContentType = "text/html";

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                blob.UploadFromStream(ms);
            }
        }

        public string ReadFromLocation(string path, string filename)
        {
            var blob = GetBlobReference(path, filename);

            string text;
            using (var memoryStream = new MemoryStream())
            {
                blob.DownloadToStream(memoryStream);
                text = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return text;
        }

        private CloudBlockBlob GetBlobReference(string path, string filename)
        {
            var cloudBlobContainer = GetCloudBlobContainer(path);
            var blob = cloudBlobContainer.GetBlockBlobReference(filename);
            return blob;
        }

        private CloudBlobContainer GetCloudBlobContainer(string path){
            var account = CloudStorageAccount.Parse(_cloudConnectionString);
            var cloudBlobClient = account.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(path);
            return cloudBlobContainer;
        }
    }
}
