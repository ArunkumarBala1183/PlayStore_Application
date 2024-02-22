using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playstore.Contracts.Data.Entities
{
    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public Guid RoleId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }
        public Guid UserId { get; set; }
    }
}