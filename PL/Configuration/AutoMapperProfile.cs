using AutoMapper;
using BLL.Models;
using BLL.Models.Account;
using DAL.Entities;
using PL.Models;
using PL.Models.Account;

namespace PL.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FileDto, FileInfoModel>().ReverseMap();
            CreateMap<UserInfoDto, UserInfoModel>().ReverseMap();
            CreateMap<SignUp, SignUpModel>()
                .ForMember(u=>u.FirstName, m=>m.MapFrom(user=>user.FirstName))
                .ForMember(u=>u.LastName, m=>m.MapFrom(user=>user.LastName))
                .ForMember(u=>u.Email, m=>m.MapFrom(user=>user.Email))
                .ForMember(u=>u.PhoneNumber, m=>m.MapFrom(user=>user.PhoneNumber))
                .ForMember(u=>u.Password, m=>m.MapFrom(user=>user.Password))
                .ReverseMap();
            CreateMap<LogIn, LogInModel>().ReverseMap();
        }
    }
}