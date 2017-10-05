using System.Collections.Concurrent;
using CB.Core;

namespace CB.InMemory
{
    public class InMemoryCacheStorage:ICacheStorage
    {
        private readonly ConcurrentDictionary<string, string> _storage = new ConcurrentDictionary<string, string>();

        public string Get(string key)
        {
            return _storage.TryGetValue(key, out string value) ? value : null;
        }

        public void Set(string key, string value)
        {
            _storage.AddOrUpdate(key, value, (oldKey, oldValue) => value);

        }

        public void Remove(string key)
        {
            _storage.TryRemove(key, out var value);
        }
    }
}
