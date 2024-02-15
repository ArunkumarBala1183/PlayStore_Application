using System.ComponentModel.DataAnnotations;

namespace Playstore.Contracts.Data.Entities
{
    public class UserCredentials
    {
        [Key]
        public Guid Id { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }

        public Guid UserId { get; set; }
        public Users User { get; set; }
    }
}