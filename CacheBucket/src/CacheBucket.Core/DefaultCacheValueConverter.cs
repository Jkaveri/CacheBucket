using System;

namespace CB.Core
{
    public class DefaultCacheValueConverter : ICacheValueConverter
    {
        /// <inheritdoc />
        public T To<T>(string input) where T : struct
        {
            var type = typeof(T);
            if (type  == typeof(int))
            {
                return (T) StringToInt(input);
            }

            if (type == typeof(float))
            {
                return (T) StringToFloat(input);
            }

            if (type == typeof(double))
            {
                return (T)StringToDouble(input);
            }

            if (type == typeof(bool))
            {
                return (T)StringToBoolean(input);
            }

            if (type == typeof(DateTime))
            {
                return (T) StringToDatetime(input);
            }

            return default(T);
        }

        private object StringToDatetime(string input)
        {
            return DateTime.TryParse(input, out var value) ? value : default(DateTime);
        }

        private object StringToBoolean(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if (input == "1")
            {
                return true;
            }

            if (input == "0")
            {
                return false;
            }

            return bool.TryParse(input, out bool result) && result;
        }

        private object StringToDouble(string input)
        {
            return double.TryParse(input, out double value) ? value : default(double);
        }

        private object StringToFloat(string input)
        {
            return float.TryParse(input, out float value) ? value : default(float);
        }


        private object StringToInt(string intStr)
        {
            return int.TryParse(intStr, out int value) ? value : default(int);
        }
    }
}