using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data.Repositories;

public class CategoryDetailsRepository : Repository<Category>, ICategoryRepository
{
    private readonly DatabaseContext context;

    public CategoryDetailsRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }
   
        public async Task<object> GetAllCategory()
        {
            var category = await context.Categories.Select(obj => new {obj.CategoryName,obj.CategoryId}).ToListAsync();
            return category;
        }

}