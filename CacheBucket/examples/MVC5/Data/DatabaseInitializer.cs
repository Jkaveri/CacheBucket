using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication.Data.Models;

namespace WebApplication.Data
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            // Users
            foreach (var user in SampleData.GetSampleUsers())
            {
                context.Users.Add(user);

                foreach (var key in SampleData.GetSampleUserPreferenceKeys())
                {
                    user.UserPreferences.Add(new UserPreference
                    {
                        Key = key,
                        Value = $"{key}-value"
                    });
                }
            }

            context.SaveChanges();
        }
    }
}