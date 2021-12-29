using System.Linq;
using Auth;
using AutoMapper;
using BLL.Models;
using BLL.Models.Account;
using DAL.Entities;

namespace BLL.Configuration
{
    public class AutoMapperProfileForBLL : Profile
    {
        public AutoMapperProfileForBLL()
        {
            CreateMap<DAL.Entities.File, FileDto>().ReverseMap();
            
            
            CreateMap<User, SignUp>()
                .ForMember(u=>u.NickName, m=>m.MapFrom(user=>user.NickName))
                .ReverseMap();
            
            /*CreateMap<UserProfile, UserProfileDto>()
                .ForMember(u=>u.Email, m=>m.MapFrom(user=>user.Email))
                .ForMember(u=>u.PhoneNumber, m=>m.MapFrom(user=>user.PhoneNumber))
                .ForMember(u=>u.Password, m=>m.MapFrom(user=>user.PasswordHash))
                .ForMember(u=>u.UserName, m=>m.MapFrom(user=>user.Email))
                .ReverseMap();*/
            
            CreateMap<UserProfile, SignUp>()
                .ForMember(u=>u.Email, m=>m.MapFrom(user=>user.Email))
                .ForMember(u=>u.PhoneNumber, m=>m.MapFrom(user=>user.PhoneNumber))
                .ForMember(u=>u.Password, m=>m.MapFrom(user=>user.PasswordHash))
                .ForMember(u=>u.UserName, m=>m.MapFrom(user=>user.Email))
                .ForMember(u=>u.FirstName, m=>m.MapFrom(user=>user.FirstName))
                .ForMember(u=>u.LastName, m=>m.MapFrom(user=>user.LastName))
                .ForMember(u=>u.NickName, m=>m.MapFrom(user=>user.NickName))
                .ReverseMap();

            /*  CreateMap<User, LogIn>()
                  .ForMember(u => u.Email, m => m.MapFrom(user => user.UserProfile.UserName))
                  .ForMember(u => u.Password, m => m.MapFrom(user => user.UserProfile.PasswordHash))
                  .ReverseMap();*/

        }
    }
}