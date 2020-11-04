using System;
using System.Linq;
using System.Web.Helpers;

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
                    new Entities.Account() { Guid = Guid.NewGuid(), Email = "user1@email.com", Password = Crypto.SHA256("111111") },
                    new Entities.Account() { Guid = Guid.NewGuid(), Email = "user2@email.com", Password = Crypto.SHA256("222222") },
                    new Entities.Account() { Guid = Guid.NewGuid(), Email = "admin@email.com", Password = Crypto.SHA256("password") }
                });
            }

            context.SaveChanges();
        }
    }
}
