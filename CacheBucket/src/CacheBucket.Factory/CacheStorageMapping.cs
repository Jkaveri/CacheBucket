#region

using System;
using System.Collections.Generic;

#endregion

namespace CB.Factory
{
    internal class CacheStorageMapping
    {
        private static readonly Lazy<CacheStorageMapping> LazyInstance = new Lazy<CacheStorageMapping>();
        private readonly Dictionary<string, Type> _map = new Dictionary<string, Type>();

        public static CacheStorageMapping Instance => LazyInstance.Value;

        public void AddMapping(string bucketName, Type type)
        {
            if (_map.ContainsKey(bucketName))
            {
                _map[bucketName] = type;
            }
            else
            {
                _map.Add(bucketName, type);
            }
        }

        public void RemoveMapping(string bucketName)
        {
            if (_map.ContainsKey(bucketName))
            {
                _map.Remove(bucketName);
            }
        }

        public Type GetStorageType(string bucketName)
        {
            if (_map.ContainsKey(bucketName))
            {
                return _map[bucketName];
            }

            throw new InvalidOperationException("Mapping doesn't exist.");
        }
    }
}