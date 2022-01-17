
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
            CreateMap<Access, AccessDto>().ReverseMap();
               
            
            CreateMap<User, SignUpDto>().ReverseMap();
            
            CreateMap<User, UserInfoDto>().ReverseMap();

            CreateMap<UserProfile, SignUpDto>()
                .ForMember(u=>u.Email, m=>m.MapFrom(user=>user.Email))
                .ForMember(u=>u.Password, m=>m.MapFrom(user=>user.PasswordHash))
                .ForMember(u=>u.UserName, m=>m.MapFrom(user=>user.Email))
                .ReverseMap();
            
        }
    }
}