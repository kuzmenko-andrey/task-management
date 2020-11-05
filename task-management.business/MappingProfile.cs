using AutoMapper;

namespace task_management.business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<data.Entities.Account, ViewModels.Account>()
                .BeforeMap((s, d) => d.Role = ViewModels.Role.User);
            CreateMap<ViewModels.Account, data.Entities.Account>()
                .BeforeMap((s, d) => d.Role = data.Entities.Role.User);
            CreateMap<ViewModels.SignUp, data.Entities.Account>()
                .BeforeMap((s, d) => d.Role = data.Entities.Role.User);
            CreateMap<ViewModels.User, data.Entities.Account>()
                .BeforeMap((s, d) => d.Role = data.Entities.Role.User);

            CreateMap<ViewModels.TaskItem, data.Entities.TaskItem>();
        }
    }
}