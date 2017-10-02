#region

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

#endregion

namespace CB.Core
{
    public class CacheBucket
    {
        private readonly ConcurrentDictionary<string, CacheBucket> _innerBuckets =
            new ConcurrentDictionary<string, CacheBucket>();

        private readonly ICacheStorage _cacheStorage;
        private string _navigationName;
        private CacheBucket _parent;


        public CacheBucket([NotNull] string name)
        {
            var rootBucketName = GetRootBucketName(name);
            var storage = CacheStorageManager.Instance.Get(rootBucketName);

            if (storage == null)
            {
                throw new InvalidOperationException($"You forgot register cache storage for cache bucket {name}.");
            }

            _cacheStorage = storage;
            Parent = GetParentBucket(name, storage, out var lastName);
            Name = lastName;
        }


        protected CacheBucket([NotNull] string name, [NotNull] ICacheStorage cacheStorage)
        {
            Name = name;
            _cacheStorage = cacheStorage;
        }

        public int ChildCount => _innerBuckets.Count;

        public string Name { get; }

        internal string NavigationName
        {
            get => _navigationName ?? (_navigationName = GenerateNavigationName());
            set => _navigationName = value;
        }

        public CacheBucket Parent
        {
            get => _parent;
            internal set
            {
                _parent = value;
                _navigationName = null;
            }
        }

        public string GetValue(string key)
        {
            key = CreateBucketName(new[] {NavigationName, key});

            return _cacheStorage.Get(key);
        }

        public CacheBucket In(string name)
        {
            if (_innerBuckets.TryGetValue(name, out var bucket))
            {
                return bucket;
            }

            bucket = new CacheBucket(name, _cacheStorage) {Parent = this};
            _innerBuckets.TryAdd(name, bucket);
            return bucket;
        }

        public void SetValue(string key, string value)
        {
            key = CreateBucketName(new[] {NavigationName, key});

            _cacheStorage.Set(key, value);
        }

        protected virtual string CreateBucketName(IEnumerable<string> names)
        {
            return names.ToBucketName();
        }

        private string GenerateNavigationName()
        {
            var names = new List<string> {Name};
            var parent = Parent;
            while (parent != null)
            {
                names.Add(parent.Name);
                parent = parent.Parent;
            }

            names.Reverse();

            return CreateBucketName(names);
        }

        private CacheBucket GetParentBucket(string name, ICacheStorage storage, out string lastName)
        {
            var bucketNames = BucketNameHelper.ExtractBucketNames(name);
            lastName = bucketNames.Last();
            if (bucketNames.Length <= 1)
            {
                return null;
            }
            CacheBucket parent = null;
            foreach (var bucketName in bucketNames.Skip(0).Take(bucketNames.Length - 1).Reverse())
            {
                var bucket = new CacheBucket(bucketName, storage)
                {
                    Parent = parent
                };
                parent = bucket;
            }

            return parent;
        }

        private string GetRootBucketName([NotNull] string name)
        {
            var bucketNames = BucketNameHelper.ExtractBucketNames(name);
            return bucketNames[0];
        }
    }
}