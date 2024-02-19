using System.Net;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Contracts.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<object> GetAllCategory();
        Task<HttpStatusCode> AddCategory(CategoryDto category);
        Task<object> SearchCategory(CategoryDto category);
        Task<object> GetCategory(Guid id);
        Task<HttpStatusCode> UpdateCategory(CategoryUpdateDto category);
    }
}