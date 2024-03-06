namespace Playstore.Contracts.DTO.TotalDownloads
{
    public record struct DownloadDetailsDto
    (
        List<string> Dates,
        List<int> count
    );
}