using CB.Core;
using JetBrains.Annotations;

namespace CB.Examples
{
    public class UserPreferenceCacheBucket:CacheBucket
    {
        public UserPreferenceCacheBucket() : base("UserPreference")
        {
        }
    }
}