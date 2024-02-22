using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playstore.Contracts.Data.Entities
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public string RefreshKey { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }
    }
}