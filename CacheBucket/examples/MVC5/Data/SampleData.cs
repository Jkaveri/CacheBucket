using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Data.Models;

namespace WebApplication.Data
{
    public static class SampleData
    {
        public static List<User> GetSampleUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "john"
                },
                new User
                {
                    Id = 2,
                    Username = "henry"
                }
            };
        }

        public static List<string> GetSampleUserPreferenceKeys()
        {
            return new List<string>
            {
                "DateTimeFormat", "TimeZone", "ProfilePicture", "PageColor", "TextColor", "FilePath", "EmailTemplate",
                "Signature", "Layout", "WeekStartDay", "CurrentPage", "RememberAllNote", "EnableSandBox", "TimeFormat",
                "DateFormat", "EmailNotificationEnabled", "SMSNotificationEnabled"
            };
        }
    }
}