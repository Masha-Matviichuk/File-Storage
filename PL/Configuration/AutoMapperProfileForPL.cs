using AutoMapper;
using BLL.Models;
using BLL.Models.Account;
using PL.Models;
using PL.Models.Account;

namespace PL.Configuration
{
    public class AutoMapperProfileForPL : Profile
    {
        public AutoMapperProfileForPL()
        {
            CreateMap<FileDto, FileInfoModel>()
                .ForMember(f => f.Access, m => m.MapFrom(file => file.Access.Modifier))
                .ReverseMap();
                
            CreateMap<UserInfoDto, UserInfoModel>().ReverseMap();
            
            CreateMap<SignUpDto, SignUpModel>()
                .ForMember(f => f.Email, m => m.MapFrom(user=>user.UserName))
                .ReverseMap();
                
            
            CreateMap<LogInDto, LogInModel>().ReverseMap();
            
            CreateMap<FileDto, CreateFileModel>().ReverseMap();

            CreateMap<AccessDto, AccessListModel>().ReverseMap();
        }
    }
}