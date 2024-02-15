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

        public AppInfo AppInfo { get; set; }
        public AppReview AppReview { get; set; }
        public AppDownloads AppDownloads { get; set; }
        public RefreshToken RefreshToken { get; set; }

    }
}