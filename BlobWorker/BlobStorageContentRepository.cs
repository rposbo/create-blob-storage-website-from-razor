using CreateFlatFileWebsiteFromRazor.Logic;

namespace BlobWorker
{
    class BlobStorageContentRepository : IContentRepository
    {
        private readonly BlobUtil _blobUtil;
        private readonly string _contentRoot;

        public BlobStorageContentRepository(string connectionString, string contentRoot)
        {
            _blobUtil = new BlobUtil(connectionString);
            _contentRoot = contentRoot;
        }

        public string GetContent(string id)
        {
            return _blobUtil.ReadFromLocation(_contentRoot, id + ".cshtml");
        }
    }
}
