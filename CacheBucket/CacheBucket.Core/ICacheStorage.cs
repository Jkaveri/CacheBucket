namespace CB.Core
{
    public interface ICacheStorage
    {
        string Get(string key);
        void Remove(string key);
        void Set(string key, string value);
    }
}