using System;
using System.Linq;
using System.Web.Helpers;

using task_management.data.Entities;

namespace task_management.data
{
    public static class DbInitializer
    {
        public static void Initialize(TaskManagerDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Accounts.Any())
            {
                context.Accounts.AddRange(new Entities.Account[] {
                    new Entities.Account() { Email = "user1@email.com", Password = Crypto.HashPassword("111111"), Role = Role.User, Username = "user1" },
                    new Entities.Account() { Email = "admin@email.com", Password = Crypto.HashPassword("password"), Role = Role.Admin, Username = "admin" }
                });
            }

            context.SaveChanges();
        }
    }
}
