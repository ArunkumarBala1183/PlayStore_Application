using System.ComponentModel.DataAnnotations;

namespace Playstore.Contracts.Data.Entities
{
    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }
        public Role Role { get; set; }
        public Guid RoleId { get; set; }

        public Users User { get; set; }
        public Guid UserId { get; set; }
    }
}