#region

using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;

#endregion

namespace CB.Core
{
    public class CacheBucket
    {
        private readonly ConcurrentDictionary<string, CacheBucket> _innerBuckets =
            new ConcurrentDictionary<string, CacheBucket>();

        private string _navigationName;
        private CacheBucket _parent;

        public CacheBucket([NotNull] string name, ICacheStorage cacheStorage)
        {
            Name = name;
            Storage = cacheStorage;
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

        internal ICacheStorage Storage { get; set; }

        public string GetValue(string key)
        {
            key = CreateBucketName(new[] {NavigationName, key});

            return Storage.Get(key);
        }

        public CacheBucket In(string name)
        {
            if (_innerBuckets.TryGetValue(name, out var bucket))
            {
                return bucket;
            }

            bucket = new CacheBucket(name, Storage) {Parent = this};
            _innerBuckets.TryAdd(name, bucket);
            return bucket;
        }

        public void SetValue(string key, string value)
        {
            key = CreateBucketName(new[] {NavigationName, key});

            Storage.Set(key, value);
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
    }
}