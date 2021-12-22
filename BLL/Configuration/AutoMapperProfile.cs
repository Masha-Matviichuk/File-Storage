using System.Linq;
using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace BLL.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<File, FileModel>().ReverseMap();
            CreateMap<User, UserInfoModel>()
                .ForMember(u => u.Id, m => m.MapFrom(user => user.Id))
                .ForMember(u=>u.FirstName, m=>m.MapFrom(user=>user.FirstName))
                .ForMember(u=>u.LastName, m=>m.MapFrom(user=>user.LastName))
                .ForMember(u=>u.Email, m=>m.MapFrom(user=>user.UserProfile.Email))
                .ForMember(u=>u.Phone, m=>m.MapFrom(user=>user.UserProfile.Phone))
                .ForMember(u=>u.Address, m=>m.MapFrom(user=>user.UserProfile.Address))
                .ForMember(u=>u.FileIds, m=>m.MapFrom(user=>user.Files.Select(x=>x.Id)))
                .ReverseMap();
            CreateMap<UserProfile, UserInfoModel>().ReverseMap();
        }
    }
}