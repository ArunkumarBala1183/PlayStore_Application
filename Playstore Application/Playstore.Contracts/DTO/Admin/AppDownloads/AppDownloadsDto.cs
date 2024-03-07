namespace Playstore.Contracts.DTO.AppDownloads
{
    public record struct AppDownloadsDto
    (
        string DownloadedDate,
        string appName,
        string userName
    );
}