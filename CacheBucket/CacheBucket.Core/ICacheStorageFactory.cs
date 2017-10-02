namespace CB.Core
{
    public interface ICacheStorageFactory
    {
        ICacheStorage Create(string bucketName);
    }
}