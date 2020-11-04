using Microsoft.Extensions.Configuration;

using System.Collections.Generic;
using System.Linq;

namespace task_management.data.Repositories
{
    public class Account
    {
        TaskManagerDbContext context;

        public Account(IConfiguration configuration)
        {
            this.context = new TaskManagerDbContext(configuration);
        }

        public IEnumerable<Entities.Account> Get()
        {
            return context.Accounts.ToList();
        }
    }
}
