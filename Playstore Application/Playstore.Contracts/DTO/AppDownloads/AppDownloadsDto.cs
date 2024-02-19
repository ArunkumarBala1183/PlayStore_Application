namespace Playstore.Contracts.DTO.AppDownloads
{
    public record struct AppDownloadsDto
    (
        DateTime DownloadedDate ,
        Guid AppId ,
        Guid UserId
    );
}