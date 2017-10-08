#region

using System.Collections.Generic;

#endregion

namespace WebApplication.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public virtual ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();
    }
}