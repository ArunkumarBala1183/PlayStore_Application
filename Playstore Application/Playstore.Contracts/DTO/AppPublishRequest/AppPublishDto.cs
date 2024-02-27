namespace Playstore.Contracts.DTO.AppPublishRequest
{
    public record struct AppPublishDto
    (
        bool Approve,
        Guid AppId
    );
}