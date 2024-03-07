namespace Playstore.Contracts.DTO
{
    public record struct RegisterUsersDTO
    (
         string Name,
         DateTime DateOfBirth, //1944 to 2006
         string EmailId,
         string MobileNumber,
         string Password,
         string ConfirmPassword
    );
}