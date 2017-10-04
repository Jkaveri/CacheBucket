using System;
using System.Collections.Generic;
using System.Text;
using CB.Core;
using CB.InMemory;
using JetBrains.Annotations;

namespace CB.Examples
{
    public class PortalSettingCacheBucket:CacheBucket
    {
        public PortalSettingCacheBucket(InMemoryCacheStorage cacheStorage) : base("PortalSetting", cacheStorage)
        {
        }
    }
}
