using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data.Repositories;

public class CategoryDetailsRepository : Repository<Category>, ICategoryRepository
{
    private readonly DatabaseContext context;

    private IMapper _mapper;

    public CategoryDetailsRepository(DatabaseContext context , IMapper mapper) : base(context)
        {
            this.context = context;
            this._mapper = mapper;
        }
   
        public async Task<object> GetAllCategory()
        {
            var category = await context.Categories.ToListAsync();
           
         var categoryDetails = this._mapper.Map<CategoryUpdateDto>(category);
         
            return categoryDetails;
        }

}