namespace Playstore.Contracts.DTO.AppData
{
    public record struct RequestedAppDataDto
    (
        byte[] AppFile,
        string ContentType
    );
}