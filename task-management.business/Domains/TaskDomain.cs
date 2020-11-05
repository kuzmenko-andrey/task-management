using AutoMapper;

using System.Collections.Generic;
using System.Linq;

namespace task_management.business.Domains
{
    public class TaskDomain
    {
        IMapper _mapper;
        data.Repositories.TaskRepository _repository;

        public TaskDomain(IMapper mapper, data.Repositories.TaskRepository repository)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        public IEnumerable<ViewModels.TaskItem> Get()
        {
            var tasks = _repository.Get();
            return tasks.Select(a => _mapper.Map<data.Entities.TaskItem, ViewModels.TaskItem>(a));
        }

        public ViewModels.TaskItem Get(int id)
        {
            var tasks = _repository.Get();
            var task = tasks.SingleOrDefault(t => t.Id == id);
            return _mapper.Map<data.Entities.TaskItem, ViewModels.TaskItem>(task);
        }

        public bool Exists(int id) => _repository.Get().Any(a => a.Id == id);

        public void Create(ViewModels.TaskItem account)
        {
            _repository.Create(_mapper.Map<data.Entities.TaskItem>(account));
        }

        public void Update(ViewModels.TaskItem account)
        {
            _repository.Update(_mapper.Map<data.Entities.TaskItem>(account));
        }

        public void Delete(int id)
        {
            _repository.Delete(new data.Entities.TaskItem { Id = id });
        }
    }
}
