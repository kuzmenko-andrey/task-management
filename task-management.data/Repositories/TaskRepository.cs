using System.Collections.Generic;
using System.Linq;

namespace task_management.data.Repositories
{
    public class TaskRepository
    {
        TaskManagerDbContext _context;

        public TaskRepository(TaskManagerDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Entities.TaskItem> Get()
        {
            return _context.Tasks.ToList();
        }

        public void Create(Entities.TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void Update(Entities.TaskItem task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void Delete(Entities.TaskItem task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}
