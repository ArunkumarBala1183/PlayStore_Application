namespace Playstore.Contracts.DTO.Category
{
    public record struct CategoryUpdateDto
    (
        Guid CategoryId,
        string CategoryName
    );

}