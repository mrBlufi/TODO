using AutoMapper;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
                .ForMember(x => x.Role, opt => opt.MapFrom(src => src.Role.Id))
                .ForMember(x => x.Hash, opt => opt.MapFrom(src => src.Hash))
                .ForMember(x => x.Salt, opt => opt.MapFrom(src => src.Salt));

            CreateMap<User, UserData>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(x => x.Role, opt => opt.MapFrom(src => new RoleData() { Id = (int)src.Role }))
                .ForMember(x => x.Hash, opt => opt.MapFrom(src => src.Hash))
                .ForMember(x => x.Salt, opt => opt.MapFrom(src => src.Salt));
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
}
