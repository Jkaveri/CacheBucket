#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace CB.Core
{
    public static class BucketNameHelper
    {
        private const string SEPARATOR = ":";

        public static string ToBucketName(this IEnumerable<string> names)
        {
            return string.Join(SEPARATOR, names.Where(s => !string.IsNullOrEmpty(s)));
        }

        internal static string[] ExtractBucketNames(string name)
        {
            return name.Split(new[] {SEPARATOR}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}