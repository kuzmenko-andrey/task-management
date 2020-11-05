using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using task_management.data.Entities;

namespace task_management.data
{
    public class TaskManagerDbContext : DbContext
    {
        IConfiguration _configuration;

        public TaskManagerDbContext(IConfiguration configuration) : base()
        {
            this._configuration = configuration;

            DbInitializer.Initialize(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
