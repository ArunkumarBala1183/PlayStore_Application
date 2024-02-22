using System.Text.Json.Serialization;
using Playstore.Contracts.DTO.AppReview;

namespace Playstore.Contracts.DTO.UserInfo
{
    public record struct UserInfoDto
    (
        string Name,
        DateOnly DateOfBirth,
        string EmailId,
        string  MobileNumber,
        ICollection<UserRoleDto> UserRoles
    );
}