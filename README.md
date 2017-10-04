# CacheBucket

A simple caching pattern.

## Usage

```csharp

public class UserPreferenceCacheBucket : CacheBucket {
	 public UserPreferenceCacheBucket(InMemoryCacheStorage cacheStorage) : base("UserPreference", cacheStorage)
    {
    }
}

public class Client {

	private readonly UserPreferenceCacheBucket _userPreferenceCache;

	public Client(UserPreferenceCacheBucket userPreferenceCache) {
		_userPreferenceCache = userPreferenceCache;
	}

	public string Get(string userId, string key) {
		var value = _userPreferenceCache.In(userId).GetValue(key);

		if (string.IsNullOrEmpty(value)) {
			return value;
		}

		var valueFromDB = "some value from db";

		_userPreferenceCache.In(userId).SetValue(key, valueFromDB);

		return valueFromDB;
	}
}
```
