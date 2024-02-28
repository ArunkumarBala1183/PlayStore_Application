using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playstore.Contracts.Data.Entities
{
    public class AppDownloads
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DownloadedDate { get; set; }
        public Guid AppId { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("AppId")]
        public AppInfo AppInfo { get; set; }
        
        [ForeignKey("UserId")]
        public Users Users { get; set; }
    }
}