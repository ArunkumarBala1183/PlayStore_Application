using AutoMapper;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using Playstore.Contracts.DTO.AppData;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Contracts.DTO.AppImages;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.AppReview;
using Playstore.Contracts.DTO.Category;
using Playstore.Contracts.DTO.UserInfo;
using Playstore.Contracts.DTO.UserRole;

namespace Playstore.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<App, AppDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Users , UserInfoDto>()
            .ForMember(dest => dest.DateOfBirth , opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")))
            .ReverseMap();
            CreateMap<AppInfo , ListAppInfoDto>().ReverseMap();
            //CreateMap<AppInfo , AppLogDetailsDto>();
            CreateMap<Users , RequestedUserDto>().ReverseMap();
            //CreateMap<Users , UserLogDetails>();
            CreateMap<Role , RoleDto>().ReverseMap();
            CreateMap<UserRole , UserRoleDto>().ReverseMap();
            CreateMap<AppReview , AppReviewDto>().ReverseMap();
            CreateMap<Category , CategoryDto>().ReverseMap();
            CreateMap<Category , CategoryUpdateDto>().ReverseMap();
            CreateMap<AppDownloads , AppDownloadsDto>()
            .ForMember(destination => destination.DownloadedDate , option => option.MapFrom(source => source.DownloadedDate.ToString("yyyy-MM-dd")))
            .ReverseMap();
            CreateMap<AppInfo , RequestAppInfoDto>()
            .ForMember(source => source.Status , option => option.MapFrom(destination => destination.Status == RequestStatus.Pending ? "Pending" : "Approved"))
            .ReverseMap();
            CreateMap<AppImages , RequestedAppImagesDto>().ReverseMap();
            CreateMap<AppData , RequestedAppDataDto>().ReverseMap();
        }
    }

}
