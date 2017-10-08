#region

using System.Collections.Generic;

#endregion

namespace WebApplication.Models
{
    public class HomeViewModel
    {
        public Dictionary<string, string> UserPreferences { get; } = new Dictionary<string, string>();
    }
}