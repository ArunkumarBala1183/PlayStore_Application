using System.ComponentModel.DataAnnotations;

namespace Playstore.Contracts.Data.Entities
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleCode { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}