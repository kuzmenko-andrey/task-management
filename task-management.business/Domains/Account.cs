using AutoMapper;
using Microsoft.Extensions.Configuration;

using System.Collections.Generic;
using System.Linq;

namespace task_management.business.Domains
{
    public class Account
    {
        IMapper mapper;
        data.Repositories.Account repository;

        public Account(IConfiguration configuration)
        {
            this.repository = new data.Repositories.Account(configuration);
            this.mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<data.Entities.Account, ViewModels.Account>();
            }).CreateMapper();
        }

        public IEnumerable<ViewModels.Account> Get()
        {
            var accounts = repository.Get();
            return accounts.Select(account => mapper.Map<data.Entities.Account, ViewModels.Account>(account));
        }
    }
}
