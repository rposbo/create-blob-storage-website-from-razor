using CreateFlatFileWebsiteFromRazor.Logic;

namespace BlobWorker
{
    class BlobStorageUploader : IUploader
    {
        private readonly BlobUtil _blobUtil;
        private readonly string _outputRoot;

        public BlobStorageUploader(string cloudConnectionString , string outputRoot)
        {
            _blobUtil = new BlobUtil(cloudConnectionString);
            _outputRoot = outputRoot;
        }
        public void SaveContentToLocation(string content, string location)
        {
            _blobUtil.SaveToLocation(content, _outputRoot, location + ".html");
        }
    }
}
