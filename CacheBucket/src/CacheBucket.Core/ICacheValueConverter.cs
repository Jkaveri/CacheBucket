using System;
using System.Collections.Generic;
using System.Text;

namespace CB.Core
{
    public interface ICacheValueConverter
    {
        /// <summary>
        /// Convert string to type.
        /// </summary>
        T To<T>(string input) where T: struct;
    }
}
