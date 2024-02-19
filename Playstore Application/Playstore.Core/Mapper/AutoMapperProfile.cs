using AutoMapper;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<App, AppDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<AppInfo , AppInfoDto>().ReverseMap();
            CreateMap<Category , CategoryDto>().ReverseMap();
            CreateMap<Category , CategoryUpdateDto>().ReverseMap();
        }
    }
}
