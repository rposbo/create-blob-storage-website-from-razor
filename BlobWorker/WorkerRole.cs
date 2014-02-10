using System.Net;
using CreateFlatFileWebsiteFromRazor.Logic;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace BlobWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            var cloudConnectionString = CloudConfigurationManager.GetSetting("Microsoft.Storage.ConnectionString");
            IContentRepository content = new BlobStorageContentRepository(cloudConnectionString, "content");
            IDataRepository data = new BlobStorageDataRespository(cloudConnectionString, "data");
            IUploader uploader = new BlobStorageUploader(cloudConnectionString, "output");

            var productIds = new[] { "1", "2", "3", "4", "5" };
            var renderer = new RenderHtmlPage(content, data);

            foreach (var productId in productIds)
            {
                var result = renderer.BuildContentResult("product", productId);
                uploader.SaveContentToLocation(result, productId);
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;
            return base.OnStart();
        }
    }
}
