using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Playstore.Contracts.Data.Entities
{
    public class AppInfo
    {
        [Key]
        public Guid AppId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
        public DateTime PublishedDate { get; set; }
        public string PublisherName { get; set; }

        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }

        public Guid RequestId { get; set; }

        public RequestStatus Status { get; set; }  
        
        
        public ICollection<AppImages> AppImages { get; set; } = new List<AppImages>();
        
        public AppData AppData { get; set; }

        public ICollection<AppReview> AppReview { get; set; } = new List<AppReview>();

        public ICollection<AppDownloads> AppDownloads { get; set; } = new List<AppDownloads>();

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; }

        [ForeignKey("RequestId")]
        public AdminRequests AdminRequests { get; set; }
        
    }
}