using AutoMapper;

using System.Collections.Generic;
using System.Linq;

namespace task_management.business.Domains
{
    public class Account
    {
        IMapper _mapper;
        data.Repositories.Account _repository;

        public Account(IMapper mapper, data.Repositories.Account repository)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        public IEnumerable<ViewModels.Account> Get()
        {
            var accounts = _repository.Get();
            return accounts.Select(a => _mapper.Map<data.Entities.Account, ViewModels.Account>(a));
        }

        public ViewModels.Account Get(int id)
        {
            var accounts = _repository.Get();
            var account = accounts.SingleOrDefault(a => a.Id == id);
            return _mapper.Map<data.Entities.Account, ViewModels.Account>(account);
        }

        public bool Exists(string email) => _repository.Get().Any(a => a.Email == email);

        public bool Exists(int id) => _repository.Get().Any(a => a.Id == id);

        public void Create(ViewModels.Account account)
        {
            _repository.Create(_mapper.Map<data.Entities.Account>(account));
        }

        public void Create(ViewModels.SignUp account)
        {
            _repository.Create(_mapper.Map<data.Entities.Account>(account));
        }

        public void Create(ViewModels.User account)
        {
            _repository.Create(_mapper.Map<data.Entities.Account>(account));
        }

        public void Update(ViewModels.Account account)
        {
            _repository.Update(_mapper.Map<data.Entities.Account>(account));
        }

        public void Update(ViewModels.User account)
        {
            _repository.Update(_mapper.Map<data.Entities.Account>(account));
        }

        public void Delete(int id)
        {
            _repository.Delete(new data.Entities.Account { Id = id });
        }
    }
}
