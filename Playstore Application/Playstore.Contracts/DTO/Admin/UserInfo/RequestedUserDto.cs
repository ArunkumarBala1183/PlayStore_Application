namespace Playstore.Contracts.DTO.UserInfo
{
    public record struct RequestedUserDto
    (
        string Name,
        string EmailId,
        string MobileNumber
    );
}