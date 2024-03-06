namespace Playstore.Contracts.DTO.UserInfo
{
    public record struct UserLogDetails
    (
        Guid UserId,
        string Name
    );
}