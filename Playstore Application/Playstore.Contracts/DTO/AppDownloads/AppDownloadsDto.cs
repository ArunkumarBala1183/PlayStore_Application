using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.UserInfo;

namespace Playstore.Contracts.DTO.AppDownloads
{
    public record struct AppDownloadsDto
    (
        string DownloadedDate,
        ListAppInfoDto AppInfo,
        UserInfoDto Users
    );
}