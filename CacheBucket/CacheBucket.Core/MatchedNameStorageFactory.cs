using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Core
{
    public class MatchedNameStorageFactory: ICacheStorageFactory
    {
        private readonly string _targetBucketName;
        private readonly Func<ICacheStorage> _factory;

        public MatchedNameStorageFactory(string bucketName, Func<ICacheStorage> factory)
        {
            _targetBucketName = bucketName;
            _factory = factory;
        }

        public ICacheStorage Create(string bucketName)
        {
            if (bucketName == _targetBucketName)
            {
                return _factory();
            }

            return null;
        }
    }
}
