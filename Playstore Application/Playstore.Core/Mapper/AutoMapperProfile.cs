using AutoMapper;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.AppReview;
using Playstore.Contracts.DTO.Category;
using Playstore.Contracts.DTO.UserInfo;

namespace Playstore.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<App, AppDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Users , UserInfoDto>().ReverseMap();
            CreateMap<AppInfo , AppInfoDto>().ReverseMap();
            CreateMap<AppReview , AppReviewDto>().ReverseMap();
            CreateMap<Category , CategoryDto>().ReverseMap();
            CreateMap<Category , CategoryUpdateDto>().ReverseMap();
            CreateMap<AppDownloads , AppDownloadsDto>().ReverseMap();
        }
    }
}
