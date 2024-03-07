using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO.AppImages;
using Playstore.Contracts.DTO.Category;
using Playstore.Contracts.DTO.UserInfo;

namespace Playstore.Contracts.DTO.AppInfo
{
    public record struct RequestAppInfoDto
    (
        Guid AppId,
        string Name,
        string Description,
        byte[] Logo,
        string Status,  
        RequestedUserDto Users,
        CategoryDto Category,
        string PublisherName,
        ICollection<RequestedAppImagesDto> AppImages

    );
}