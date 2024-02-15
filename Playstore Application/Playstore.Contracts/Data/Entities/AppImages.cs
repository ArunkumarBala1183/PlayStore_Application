using System.ComponentModel.DataAnnotations;

namespace Playstore.Contracts.Data.Entities
{
    public class AppImages
    {
        [Key]
        public Guid AppImageId { get; set; }
        public byte[] Image { get; set; }
        public Guid AppId { get; set; }
        public AppInfo AppInfo { get; set; }
    }
}