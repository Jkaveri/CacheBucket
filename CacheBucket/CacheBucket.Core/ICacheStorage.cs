namespace CB.Core
{
    public interface ICacheStorage
    {
        string Get(string key);
        void Set(string key, string value);
        void Remove(string key);
    }
}