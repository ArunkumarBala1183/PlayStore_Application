using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.UserInfo;
using Playstore.Contracts.DTO.UserRole;

namespace Playstore.Contracts.DTO.AppDownloads
{
    public record struct AppDownloadsDto
    (
        string DownloadedDate,
        string appName,
        string userName
        // AppLogDetailsDto AppInfo,
        // UserLogDetails Users
    );
}