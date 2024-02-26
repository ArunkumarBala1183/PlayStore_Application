using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;
        public CategoryRepository(DatabaseContext context , IMapper mapper)
        {
            this.database = context;
            this.mapper = mapper;
        }
        public async Task<HttpStatusCode> AddCategory(CategoryDto category)
        {
            if (! await this.IsAlreadyExistedCategory(category))
            {
                var categoryDetails = this.mapper.Map<Category>(category); 
                
                await this.database.Categories.AddAsync(categoryDetails);
                await this.database.SaveChangesAsync();
    
                return HttpStatusCode.Created;
            }

            return HttpStatusCode.AlreadyReported;
        }

        private async Task<bool> IsAlreadyExistedCategory(CategoryDto category)
        {
            var existedData = await this.database.Categories.Where(options => options.CategoryName.ToLower() == category.CategoryName.ToLower()).FirstOrDefaultAsync();

            if(existedData != null)
            {
                return true;
            }

            return false;
        }

        public async Task<object> SearchCategory(CategoryDto category)
        {
            var categoryDetails = this.mapper.Map<Category>(category);

            var searchedDetails = await this.database.Categories.Where(searchOption => searchOption.CategoryName.Contains(category.CategoryName)).Select(id => id.CategoryName).ToListAsync();

            if (searchedDetails != null && searchedDetails.Count > 0)
            {
                return searchedDetails;
            }

            return HttpStatusCode.NoContent;
        }

        public async Task<object> GetCategory(Guid id)
        {
            var searchedDetails = await this.database.Categories.FindAsync(id);

            if (searchedDetails != null)
            {
                var categoryDto = this.mapper.Map<CategoryUpdateDto>(searchedDetails);
                return categoryDto;
            }

            return HttpStatusCode.NoContent;
        }

        public async Task<HttpStatusCode> UpdateCategory(CategoryUpdateDto category)
        {
            var categoryDetails = this.mapper.Map<Category>(category);

            this.database.Categories.Attach(categoryDetails);

            this.database.Entry(categoryDetails).State = EntityState.Modified;

            await this.database.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<object> GetAllCategory()
        {
            var existedData = await this.database.Categories.ToListAsync();

            if(existedData != null)
            {
                var categoryDto = this.mapper.Map<IEnumerable<CategoryUpdateDto>>(existedData);
                return categoryDto;
            }

            return HttpStatusCode.NoContent;
        }
    }
}