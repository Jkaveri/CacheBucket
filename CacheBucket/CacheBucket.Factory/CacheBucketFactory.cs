#region

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using CB.Core;

#endregion

namespace CB.Factory
{
   
    public class CacheBucketFactory
    {
        private readonly IServiceProvider _sp;

        public CacheBucketFactory(IServiceProvider sp)
        {
            _sp = sp;
        }

        public CacheBucket Create(string name)
        {
            var names = BucketNameHelper.ExtractBucketNames(name);
            var rootName = names[0];
            var storageType = CacheStorageMapping.Instance.GetStorageType(rootName);
            var storage = _sp.GetService(storageType) as ICacheStorage;

            var parent = GetParentBucket(name, storage, out var lastName);

            var bucket = new CacheBucket(lastName, storage) {Parent = parent};
            return bucket;
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
    }
}