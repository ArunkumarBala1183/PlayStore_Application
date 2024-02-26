namespace Playstore.Contracts.DTO.AppDownloads
{
    public record struct AppLogsDto
    (
        DateTime? DownloadedDate,
        Guid? AppId,
        Guid? UserId
    );
}