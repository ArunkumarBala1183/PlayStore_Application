
namespace Playstore.Contracts.DTO
{
    public class UserCredentialsDTO
    {
        public Guid Id { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleCode { get; set; } = "User";
    }
}