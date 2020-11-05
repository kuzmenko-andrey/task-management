using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using task_management.data.Entities;

namespace task_management.data
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(IConfiguration configuration) : base()
        {
            DbInitializer.Initialize(this);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
