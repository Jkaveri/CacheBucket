#region

using CB.Core;
using CB.InMemory;
using FluentAssertions;
using Xunit;

#endregion

namespace CB.Tests
{
    public class CacheBucketTests
    {
        private const string BUCKET_NAME_1 = "UserPreference";
        private const string BUCKET_NAME_2 = "TestBucket2";

        [Fact]
        public void Should_consitent_value_between_2_instances()
        {
            // Arrange
            var storage = new InMemoryCacheStorage();
            var testKey = "A";
            var testValue = "B";
            var testValue2 = "C";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1, storage);
            var bucket2 = new CacheBucket(BUCKET_NAME_1, storage);

            bucket.SetValue(testKey, testValue);
            bucket2.SetValue(testKey, testValue2);

            // Asserts
            bucket.GetValue(testKey)
                .Should().Be(bucket2.GetValue(testKey))
                .And.Subject.Should().Be(testValue2);
        }

        [Fact]
        public void should_create_bucket_success()
        {
            // Arrange
            var storage = new InMemoryCacheStorage();
            var testKey = "A";
            var testValue = "B";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1, storage);
            bucket.SetValue(testKey, testValue);

            // Asserts
            bucket.Name.Should().Be(BUCKET_NAME_1);
            bucket.GetValue(testKey).Should().Be(testValue);
        }

        [Fact]
        public void should_return_correct_value_when_quick_access()
        {
            // Arrange
            var storage = new InMemoryCacheStorage();
            var testKey = "A";
            var childContainer = "1";
            var testValue2 = "C";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1, storage);
            bucket.In(childContainer).SetValue(testKey, testValue2);

            var childBucket = new CacheBucket(new[] {BUCKET_NAME_1, childContainer}.ToBucketName(), storage);


            // Asserts
            bucket.In(childContainer).GetValue(testKey).Should().Be(testValue2);
            childBucket.GetValue(testKey).Should().Be(testValue2);
        }

        [Fact]
        public void should_return_correct_values()
        {
            // Arrange
            var storage = new InMemoryCacheStorage();
            var testKey = "A";
            var childContainer = "1";
            var testValue = "B";
            var testValue2 = "C";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1, storage);

            bucket.SetValue(testKey, testValue);
            bucket.In(childContainer).SetValue(testKey, testValue2);

            // Asserts
            testValue.Should().NotBe(testValue2);
            bucket.GetValue(testKey).Should().Be(testValue);
            bucket.In(childContainer).GetValue(testKey).Should().Be(testValue2);
        }

        [Fact]
        public void should_return_different_value_from_different_buckets()
        {
            // Arrange
            var storage = new InMemoryCacheStorage();
            var testKey = "A";
            var testValue = "B";
            var testValue2 = "C";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1, storage);
            var bucket2 = new CacheBucket(BUCKET_NAME_2, storage);

            bucket.SetValue(testKey, testValue);
            bucket2.SetValue(testKey, testValue2);

            // Asserts
            testValue.Should().NotBe(testValue2);
            bucket.GetValue(testKey).Should().Be(testValue);
            bucket2.GetValue(testKey).Should().Be(testValue2);
        }

        [Fact]
        public void should_return_value_in_other_class()
        {
            // Arrange
            var storage = new InMemoryCacheStorage();
            var testKey = "A";
            var testValue = "B";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1, storage);
            var bucket2 = new CacheBucket(BUCKET_NAME_1, storage);
            bucket.SetValue(testKey, testValue);

            // Asserts
            bucket2.GetValue(testKey).Should().Be(testValue);
        }
    }
}