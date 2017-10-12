#region

using System;
using CB.Core;
using FluentAssertions;
using Xunit;

#endregion

namespace CB.Tests
{
    public class CacheValueConverterTests
    {
        [Theory(DisplayName = "Convert string to bool: ")]
        [InlineData("-1", default(bool))]
        [InlineData("0", false)]
        [InlineData("1", true)]
        [InlineData("1 ", default(bool))]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData(null, default(bool))]
        [InlineData("", default(bool))]
        [InlineData(" ", default(bool))]
        public void should_convert_string_to_bool(string input, bool expected)
        {
            ICacheValueConverter converter = new DefaultCacheValueConverter();

            // 
            var result = converter.To<bool>(input);

            //
            result.Should().Be(expected);
        }

        [Theory(DisplayName = "Convert string to datetime: ")]
        [InlineData("2008-05-01T07:34:42-5:00")]
        [InlineData("2008-05-01 7:34:42Z")]
        [InlineData("Thu, 01 May 2008 07:34:42 GMT")]
        [InlineData("2013-02-08")]
        [InlineData("20130208")]
        public void should_convert_string_to_datetime(string input)
        {
            ICacheValueConverter converter = new DefaultCacheValueConverter();
            DateTime.TryParse(input, out DateTime expected);

            // 
            var result = converter.To<DateTime>(input);

            //
            result.Should().Be(expected);
        }

        [Theory(DisplayName = "Convert string to double: ")]
        [InlineData("-1", -1D)]
        [InlineData("0", 0D)]
        [InlineData("1", 1D)]
        [InlineData("01", 1D)]
        [InlineData("1.1", 1.1D)]
        [InlineData("a", 0D)]
        [InlineData(" ", 0D)]
        [InlineData(".1", 0.1D)]
        [InlineData(null, 0D)]
        public void should_convert_string_to_double(string input, double expected)
        {
            ICacheValueConverter converter = new DefaultCacheValueConverter();

            // 
            var result = converter.To<double>(input);

            //
            result.Should().Be(expected);
        }

        [Theory(DisplayName = "Convert string to float: ")]
        [InlineData("-1", -1F)]
        [InlineData("0", 0F)]
        [InlineData("1", 1F)]
        [InlineData("01", 1F)]
        [InlineData("1.1", 1.1F)]
        [InlineData("a", 0F)]
        [InlineData(" ", 0F)]
        [InlineData(".1", 0.1F)]
        [InlineData(null, 0F)]
        public void should_convert_string_to_float(string input, float expected)
        {
            ICacheValueConverter converter = new DefaultCacheValueConverter();

            // 
            var result = converter.To<float>(input);

            //
            result.Should().Be(expected);
        }

        [Theory(DisplayName = "Convert string to int: ")]
        [InlineData("-1", -1)]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("01", 1)]
        [InlineData("1.1", 0)]
        [InlineData("a", 0)]
        [InlineData(" ", 0)]
        [InlineData(".1", 0)]
        [InlineData(null, 0)]
        public void should_convert_string_to_int(string input, int expected)
        {
            ICacheValueConverter converter = new DefaultCacheValueConverter();

            // 
            var result = converter.To<int>(input);

            //
            result.Should().Be(expected);
        }
    }
}