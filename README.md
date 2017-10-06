# CacheBucket

A simple caching pattern.

|Branches|Build Status|
|--------|------------|
|Develop |[![Build Status](https://travis-ci.org/Jkaveri/CacheBucket.svg?branch=develop)](https://travis-ci.org/Jkaveri/CacheBucket)|
|Master  |[![Build Status](https://travis-ci.org/Jkaveri/CacheBucket.svg?branch=master)](https://travis-ci.org/Jkaveri/CacheBucket) [![Build status](https://ci.appveyor.com/api/projects/status/q8v2rykkky6tc6s9/branch/master?svg=true)](https://ci.appveyor.com/project/Jkaveri/cachebucket/branch/master)
|

## Usage

```csharp

public class UserPreferenceCacheBucket : CacheBucket {
	 public UserPreferenceCacheBucket(InMemoryCacheStorage cacheStorage) : base("UserPreference", cacheStorage)
    {
    }
}

public class Client {

	private readonly UserPreferenceCacheBucket _userPreferenceCache;

	// This argument will be injected by DI container
	public Client(UserPreferenceCacheBucket userPreferenceCache) {
		_userPreferenceCache = userPreferenceCache;
	}

	public string Get(string userId, string key) {
		var value = _userPreferenceCache.In(userId).GetValue(key);

		if (!string.IsNullOrEmpty(value)) {
			return value;
		}

		var valueFromDB = "some value from db";

		_userPreferenceCache.In(userId).SetValue(key, valueFromDB);

		return valueFromDB;
	}
}
```
