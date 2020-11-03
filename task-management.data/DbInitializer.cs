using System;
using System.Collections.Generic;
using System.Text;

namespace task_management.data
{
    public static class DbInitializer
    {
        public static void Initialize(TaskManagerDbContext context)
        {
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
