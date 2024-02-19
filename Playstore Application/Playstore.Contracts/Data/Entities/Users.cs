using System.ComponentModel.DataAnnotations;

namespace Playstore.Contracts.Data.Entities
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public string  MobileNumber { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public UserCredentials userCredentials { get; set; }

        public List<AppInfo> AppInfo { get; set; } = new List<AppInfo>();
        public AppReview AppReview { get; set; }
        public List<AppDownloads> AppDownloads { get; set; } = new List<AppDownloads>();
        public RefreshToken RefreshToken { get; set; }

    }
}