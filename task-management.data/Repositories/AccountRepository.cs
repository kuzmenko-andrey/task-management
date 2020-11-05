using System.Collections.Generic;
using System.Linq;

namespace task_management.data.Repositories
{
    public class AccountRepository
    {
        TaskManagerDbContext _context;

        public AccountRepository(TaskManagerDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Entities.Account> Get()
        {
            return _context.Accounts.ToList();
        }

        public void Create(Entities.Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public void Update(Entities.Account account)
        {
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }

        public void Delete(Entities.Account account)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}
