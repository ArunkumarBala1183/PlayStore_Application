namespace Playstore.Contracts.DTO
{
    public record struct RegisterUsersDTO
    (
         string Name,
         DateTime DateOfBirth, 
         string EmailId,
         string MobileNumber,
         string Password,
         string ConfirmPassword
    );
}