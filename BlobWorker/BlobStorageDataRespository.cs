using CreateFlatFileWebsiteFromRazor.Logic;

namespace BlobWorker
{
    public class BlobStorageDataRespository : IDataRepository
    {
        private readonly BlobUtil _blobUtil;
        private readonly string _dataRoot;

        public BlobStorageDataRespository(string connectionString, string dataRoot)
        {
            _blobUtil = new BlobUtil(connectionString);
            _dataRoot = dataRoot;
        }

        public string GetData(string id)
        {
            return _blobUtil.ReadFromLocation(_dataRoot, id + ".json");
        }
    }
}
