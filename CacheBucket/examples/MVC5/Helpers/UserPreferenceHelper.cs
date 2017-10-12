using System.Linq;
using CB.Core;
using WebApplication.Data;

namespace WebApplication.Helpers
{
    public class UserPreferenceHelper
    {
        private readonly UserPreferenceCacheBucket _cacheBucket;
        private readonly ApplicationDbContext _dbContext;


        public UserPreferenceHelper(UserPreferenceCacheBucket cacheBucket, ApplicationDbContext dbContext)
        {
            _cacheBucket = cacheBucket;
            _dbContext = dbContext;
        }

        public string Get(int userId, string key)
        {
            CacheBucket userBucket = null;
            if (MvcApplication.EnableCacheBucket)
            {

                userBucket = _cacheBucket.In(userId.ToString());

                // get cache value.
                var cacheValue = userBucket.GetValue(key);

                if (string.IsNullOrEmpty(cacheValue) == false)
                {
                    return cacheValue;
                }
            }

            // get db value.
            Data.Models.UserPreference userPreference = GetUserPreference(userId, key);

            if (userPreference == null)
            {
                return null;
            }

            // set into cache.
            userBucket?.SetValue(key, userPreference.Value);

            return userPreference.Value;
        }

        public void Set(int userId, string key, string value)
        {
            var userPreference = GetUserPreference(userId, key);
            CacheBucket userBucket = null;
            if (MvcApplication.EnableCacheBucket)
            {
                userBucket = _cacheBucket.In(userId.ToString());
            }

            if (userPreference == null)
            {
                AddUserPreference(userId, key, value);
            }
            else
            {
                userPreference.Value = value;
            }

            // commit
            _dbContext.SaveChanges();

            // set cache.
            userBucket?.SetValue(key, value);

        }

        private Data.Models.UserPreference GetUserPreference(int userId, string key)
        {
            return _dbContext.UserPreferences.FirstOrDefault(t => t.UserId == userId && t.Key == key);
        }

        private Data.Models.UserPreference AddUserPreference(int userId, string key, string value)
        {
            var userPreference = new Data.Models.UserPreference
            {
                UserId = userId,
                Key = key,
                Value = value
            };

            _dbContext.UserPreferences.Add(userPreference);
            
            return userPreference;
        }
    }
}