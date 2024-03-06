using AutoMapper;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<App, AppDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<AppInfo,AppDownloadDataDto>();
            CreateMap<AppData,AppDownloadDataDto>();
            CreateMap<AppDownloads,AppDownloadDetailsDto>();
            CreateMap<Category,CategoryUpdateDto>().ReverseMap();
            
        }
    }
}
