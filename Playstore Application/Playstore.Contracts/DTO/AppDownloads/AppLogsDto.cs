namespace Playstore.Contracts.DTO.AppDownloads
{
    public record struct AppLogsDto
    (
        DateTime? FromDate,
        DateTime? DownloadedDate,
        string UserName,
        string AppName
    );
}