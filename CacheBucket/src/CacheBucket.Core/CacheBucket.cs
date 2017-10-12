#region

using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;

#endregion

namespace CB.Core
{
    public class CacheBucket
    {
        [NotNull] private readonly ICacheStorage _cacheStorage;
        [NotNull] private readonly ICacheValueConverter _converter;

        private readonly ConcurrentDictionary<string, CacheBucket> _innerBuckets =
            new ConcurrentDictionary<string, CacheBucket>();

        [NotNull] private readonly string _name;

        private string _navigationName;
        private CacheBucket _parent;

        /// <summary>
        /// Intialize cache bucket class with <see cref="DefaultCacheValueConverter"/>.
        /// </summary>
        public CacheBucket([NotNull] string name, [NotNull] ICacheStorage cacheStorage) : this(name, cacheStorage,
            new DefaultCacheValueConverter())
        {
        }

        /// <summary>
        /// Initialize cache bucket class.
        /// </summary>
        public CacheBucket([NotNull] string name, [NotNull] ICacheStorage cacheStorage,
            [NotNull] ICacheValueConverter converter)
        {
            _name = name;
            _cacheStorage = cacheStorage;
            _converter = converter;
        }

        public int ChildCount => _innerBuckets.Count;
        public ICacheValueConverter Converter => _converter;
        public string Name => _name;

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

        public ICacheStorage Storage => _cacheStorage;

        public T Get<T>(string key) where T : struct
        {
            var str = GetValue(key);

            return Converter.To<T>(str);
        }

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