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

        private void SetupBuckets()
        {
            CacheBucketManager.With(() => new InMemoryCacheStorage())
                .Register(BUCKET_NAME_1)
                .Register(BUCKET_NAME_2);
        }

        [Fact]
        public void Should_consitent_value_between_2_instances()
        {
            // Arrange
            SetupBuckets();
            var testKey = "A";
            var testValue = "B";
            var testValue2 = "C";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1);
            var bucket2 = new CacheBucket(BUCKET_NAME_1);

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
            SetupBuckets();
            var testKey = "A";
            var testValue = "B";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1);
            bucket.SetValue(testKey, testValue);

            // Asserts
            bucket.Name.Should().Be(BUCKET_NAME_1);
            bucket.GetValue(testKey).Should().Be(testValue);
        }

        [Fact]
        public void should_return_correct_value_when_quick_access()
        {
            // Arrange
            SetupBuckets();
            var testKey = "A";
            var childContainer = "1";
            var testValue2 = "C";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1);
            bucket.In(childContainer).SetValue(testKey, testValue2);

            var childBucket = new CacheBucket(new[] {BUCKET_NAME_1, childContainer}.ToBucketName());


            // Asserts
            bucket.In(childContainer).GetValue(testKey).Should().Be(testValue2);
            childBucket.GetValue(testKey).Should().Be(testValue2);
        }

        [Fact]
        public void should_return_correct_values()
        {
            // Arrange
            SetupBuckets();
            var testKey = "A";
            var childContainer = "1";
            var testValue = "B";
            var testValue2 = "C";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1);

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
            SetupBuckets();
            var testKey = "A";
            var testValue = "B";
            var testValue2 = "C";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1);
            var bucket2 = new CacheBucket(BUCKET_NAME_2);

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
            SetupBuckets();
            var testKey = "A";
            var testValue = "B";

            // Actions
            var bucket = new CacheBucket(BUCKET_NAME_1);
            var bucket2 = new CacheBucket(BUCKET_NAME_1);
            bucket.SetValue(testKey, testValue);

            // Asserts
            bucket2.GetValue(testKey).Should().Be(testValue);
        }
    }
}