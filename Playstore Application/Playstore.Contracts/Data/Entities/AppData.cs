using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playstore.Contracts.Data.Entities
{
    public class AppData
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] AppFile { get; set; }
        public string ContentType { get; set; }
        public Guid AppId { get; set; }
        
        [ForeignKey("AppId")] 
        public AppInfo AppInfo { get; set; }
    }
}