using System.ComponentModel.DataAnnotations;

namespace Playstore.Contracts.Data.Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        public ICollection<AppInfo> AppInfo { get; set; } = new List<AppInfo>();
    }
}