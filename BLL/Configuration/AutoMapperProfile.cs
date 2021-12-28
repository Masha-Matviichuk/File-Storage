using System.Linq;
using AutoMapper;
using BLL.Models;
using BLL.Models.Account;
using DAL.Entities;

namespace BLL.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.Entities.File, FileDto>().ReverseMap();
            CreateMap<User, UserInfoDto>()
                .ForMember(u => u.Id, m => m.MapFrom(user => user.Id))
                .ForMember(u=>u.FirstName, m=>m.MapFrom(user=>user.FirstName))
                .ForMember(u=>u.LastName, m=>m.MapFrom(user=>user.LastName))
                .ForMember(u=>u.Email, m=>m.MapFrom(user=>user.UserProfile.Email))
                .ForMember(u=>u.PhoneNumber, m=>m.MapFrom(user=>user.UserProfile.PhoneNumber))
                .ForMember(u=>u.Password, m=>m.MapFrom(user=>user.UserProfile.PasswordHash))
                .ForMember(u=>u.UserName, m=>m.MapFrom(user=>user.UserProfile.Email))
                .ReverseMap();
            
            CreateMap<UserProfile, UserInfoDto>().ReverseMap();
            
            CreateMap<User, SignUp>()
                .ForMember(u=>u.FirstName, m=>m.MapFrom(user=>user.FirstName))
                .ForMember(u=>u.LastName, m=>m.MapFrom(user=>user.LastName))
                .ForMember(u=>u.Email, m=>m.MapFrom(user=>user.UserProfile.Email))
                .ForMember(u=>u.PhoneNumber, m=>m.MapFrom(user=>user.UserProfile.PhoneNumber))
                .ForMember(u=>u.Password, m=>m.MapFrom(user=>user.UserProfile.PasswordHash))
                .ForMember(u=>u.UserName, m=>m.MapFrom(user=>user.UserProfile.Email))
                .ReverseMap();

          /*  CreateMap<User, LogIn>()
                .ForMember(u => u.Email, m => m.MapFrom(user => user.UserProfile.UserName))
                .ForMember(u => u.Password, m => m.MapFrom(user => user.UserProfile.PasswordHash))
                .ReverseMap();*/

        }
    }
}