using AutoMapper;

namespace task_management.business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<data.Entities.Account, ViewModels.Account>();
            CreateMap<ViewModels.Account, data.Entities.Account>();
            CreateMap<ViewModels.SignUp, data.Entities.Account>();
            CreateMap<ViewModels.User, data.Entities.Account>();

            CreateMap<ViewModels.TaskItem, data.Entities.TaskItem>();
        }
    }
}