using AutoMapper;
using BLL.Models;
using BLL.Models.Account;
using DAL.Entities;
using PL.Models;
using PL.Models.Account;

namespace PL.Configuration
{
    public class AutoMapperProfileForPL : Profile
    {
        public AutoMapperProfileForPL()
        {
            CreateMap<FileDto, FileInfoModel>().ReverseMap();
            CreateMap<UserInfoDto, UserInfoModel>().ReverseMap();
            CreateMap<SignUp, SignUpModel>()
                .ForMember(u => u.FirstName, m => m.MapFrom(user => user.FirstName))
                .ForMember(u => u.LastName, m => m.MapFrom(user => user.LastName))
                .ForMember(u => u.Email, m => m.MapFrom(user => user.Email))
                .ForMember(u => u.PhoneNumber, m => m.MapFrom(user => user.PhoneNumber))
                .ForMember(u => u.Password, m => m.MapFrom(user => user.Password))
                .ForMember(u => u.Email, m => m.MapFrom(user => user.UserName))
                .ReverseMap();
            CreateMap<LogIn, LogInModel>().ReverseMap();
            
            CreateMap<FileDto, CreateFileModel>()
                .ForMember(f=>f.Title, m => m.MapFrom(file => file.Title))
                .ForMember(f=>f.Description, m => m.MapFrom(file => file.Description))
                .ForMember(f=>f.AccessId, m => m.MapFrom(file => file.AccessId))
                .ReverseMap();
            CreateMap<LogIn, LogInModel>().ReverseMap();
        }
    }
}