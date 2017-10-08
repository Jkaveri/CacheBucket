#region

#endregion

namespace WebApplication.Data.Models
{
    public class UserPreference
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public string Value { get; set; }
    }
}