using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Playstore.Contracts.Data.Entities
{
    public class AppImages
    {
        [Key]
        public Guid AppImageId { get; set; }
         public byte[] Image { get; set; }
        public Guid AppId { get; set; }
         [ForeignKey("AppId")] 
        public AppInfo AppInfo { get; set; }

       
    }
}