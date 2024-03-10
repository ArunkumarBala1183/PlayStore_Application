using Playstore.Contracts.DTO.Category;

namespace Playstore.Contracts.DTO.AppInfo
{
  public record struct ListAppInfoDto
  (
      Guid AppId,
      string Name,
      byte[] Logo,
      string Description,
      int Rating,
      int Downloads,
      CategoryDto Category,
      bool UserDownloaded
  );
}