namespace Playstore.Contracts.DTO.AppInfo
{
    public record struct AppInfoDto
    (
      Guid AppId,
      string Name,
      byte[] Logo,
      Guid CategoryId  
      //Review Should be add
    );
}