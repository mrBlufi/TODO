using AutoMapper;
using DataAccessLayer.Models;

namespace BusinessSolutionsLayer.Models
{
    public class RoleProfile : Profile
    {
        public RoleProfile() => CreateMap<RoleData, Role>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description));
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserData, User>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(x => x.Role, opt => opt.MapFrom(src => src.Role.Id));

            CreateMap<User, UserData>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(x => x.Role, opt => opt.MapFrom(src => new RoleData() { Id = (int)src.Role }));
        }
    }

    public class TaskProfile : Profile
    {
        public TaskProfile() => CreateMap<TaskData, Task>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.CreateBy, opt => opt.MapFrom(src => src.CreateBy));
    }

    public class SessionProfile : Profile
    {
        public SessionProfile() => CreateMap<SessionData, Session>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.User, opt => opt.MapFrom(src => src.User))
            .ForMember(x => x.ExperationTime, opt => opt.MapFrom(src => src.ExperationTime))
            .ForMember(x => x.CreatingTime, opt => opt.MapFrom(src => src.CreatingTime));
    }
}
