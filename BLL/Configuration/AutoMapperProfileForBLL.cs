using System.Linq;
using Auth;
using Auth.Entities;
using AutoMapper;
using BLL.Models;
using BLL.Models.Account;
using DAL.Entities;

namespace BLL.Configuration
{
    public class AutoMapperProfileForBll : Profile
    {
        public AutoMapperProfileForBll()
        {
            CreateMap<DAL.Entities.File, FileDto>().ReverseMap();
            CreateMap<Access, AccessDto>()
                .ForMember(a=>a.Id, m=>m.MapFrom(access=>access.Id))
                .ForMember(a=>a.Modifier, m=>m.MapFrom(access=>access.Modifier))
                .ReverseMap();
               
            
            CreateMap<User, SignUpDto>()
                .ForMember(u=>u.Email, m=>m.MapFrom(user=>user.Email))
                .ReverseMap();
            
            CreateMap<User, UserInfoDto>()
                .ForMember(u=>u.Email, m=>m.MapFrom(user=>user.Email))
                .ReverseMap();

            CreateMap<UserProfile, SignUpDto>()
                .ForMember(u=>u.Email, m=>m.MapFrom(user=>user.Email))
                .ForMember(u=>u.PhoneNumber, m=>m.MapFrom(user=>user.PhoneNumber))
                .ForMember(u=>u.Password, m=>m.MapFrom(user=>user.PasswordHash))
                .ForMember(u=>u.UserName, m=>m.MapFrom(user=>user.Email))
                .ForMember(u=>u.FirstName, m=>m.MapFrom(user=>user.FirstName))
                .ForMember(u=>u.LastName, m=>m.MapFrom(user=>user.LastName))
                .ReverseMap();
            
        }
    }
}