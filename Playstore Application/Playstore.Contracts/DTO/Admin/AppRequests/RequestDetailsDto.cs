namespace Playstore.Contracts.DTO.AppRequests
{
    public record struct RequestDetailsDto
    (
        Guid AppId,
        string Name,
        byte[] Logo,
        string Description,
        string Category
    );
}