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
            CreateMap<DAL.Entities.File, FileDto>().ReverseMap();
            CreateMap<User, UserInfoDto>()
                .ForMember(u => u.Id, m => m.MapFrom(user => user.Id))
                .ForMember(u=>u.FirstName, m=>m.MapFrom(user=>user.FirstName))
                .ForMember(u=>u.LastName, m=>m.MapFrom(user=>user.LastName))
                .ForMember(u=>u.FileIds, m=>m.MapFrom(user=>user.Files.Select(x=>x.Id)))
                .ReverseMap();
            
        }
    }
}