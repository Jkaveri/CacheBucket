#region

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace CB.Core
{
    internal class CacheStorageManager
    {
        private static readonly Lazy<CacheStorageManager> LazyInstance =
            new Lazy<CacheStorageManager>(() => new CacheStorageManager());

        private readonly List<ICacheStorageFactory> _factories
            = new List<ICacheStorageFactory>();

        private readonly ConcurrentDictionary<string, ICacheStorage> _cacheStorages =
            new ConcurrentDictionary<string, ICacheStorage>();

        private CacheStorageManager()
        {
        }

        public static CacheStorageManager Instance => LazyInstance.Value;

        internal void AddStorageFactory(ICacheStorageFactory factory)
        {
            _factories.Add(factory);
        }

        internal ICacheStorage Get(string bucketName)
        {
            if (_cacheStorages.TryGetValue(bucketName, out ICacheStorage storage))
            {
                return storage;
            }

            storage = _factories.Select(factory => factory.Create(bucketName))
                .FirstOrDefault(s => s != null);

            if (!_cacheStorages.TryAdd(bucketName, storage))
            {
                throw new InvalidOperationException("Unable to remember cache storage!");
            }

            return storage;
        }
    }
}