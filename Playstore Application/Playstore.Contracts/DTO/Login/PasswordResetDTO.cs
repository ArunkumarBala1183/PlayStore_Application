using System.Text.Json.Serialization;

namespace Playstore.Contracts.DTO
{
    public class PasswordResetDTO
    {
        [JsonIgnore]
        public string EmailId { get; set; }
        [JsonIgnore]
        public string Otp { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
