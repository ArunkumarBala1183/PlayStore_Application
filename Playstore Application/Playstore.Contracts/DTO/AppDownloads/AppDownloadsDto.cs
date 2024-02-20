using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.UserInfo;

namespace Playstore.Contracts.DTO.AppDownloads
{
    public record struct AppDownloadsDto
    (
        DateTime DownloadedDate,
        Guid AppId ,
        Guid UserId
        // AppInfoDto AppInfo,
        // UserInfoDto UserInfo
    );
}