using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext database;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        public CategoryRepository(DatabaseContext context, IMapper mapper , IHttpContextAccessor httpContext)
        {
            this.database = context;
            this.mapper = mapper;
            logger = Log.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(CategoryRepository).Name);
        }
        public async Task<HttpStatusCode> AddCategory(CategoryDto category)
        {
            try
            {
                if (!await this.IsAlreadyExistedCategory(category))
                {
                    var categoryDetails = this.mapper.Map<Category>(category);

                    await this.database.Categories.AddAsync(categoryDetails);
                    await this.database.SaveChangesAsync();
                    logger.Information($"{category.CategoryName} Added successfully");

                    return HttpStatusCode.Created;
                }
                logger.Information("Category Already Reported");
                return HttpStatusCode.AlreadyReported;
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }

        private async Task<bool> IsAlreadyExistedCategory(CategoryDto category)
        {
            var existedData = await this.database.Categories.Where(options => options.CategoryName.ToLower() == category.CategoryName.ToLower()).FirstOrDefaultAsync();

            if (existedData != null)
            {
                return true;
            }

            return false;
        }

        public async Task<object> SearchCategory(CategoryDto category)
        {
            try
            {    
                var searchedDetails = await this.database.Categories.Where(searchOption => searchOption.CategoryName.Contains(category.CategoryName)).Select(id => id.CategoryName).ToListAsync();
    
                if (searchedDetails != null && searchedDetails.Count > 0)
                {
                    logger.Information("Category Fetched from Server");
                    return searchedDetails;
                }
                logger.Information("No Category Found");
                return HttpStatusCode.NotFound;
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<object> GetCategory(Guid id)
        {
            try
            {
                var searchedDetails = await this.database.Categories.FindAsync(id);
    
                if (searchedDetails != null)
                {
                    var categoryDto = this.mapper.Map<CategoryUpdateDto>(searchedDetails);
                    logger.Information("Category Fetched from Server");
                    return categoryDto;
                }
                logger.Information("No Category Found");
                return HttpStatusCode.NotFound;
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<HttpStatusCode> UpdateCategory(CategoryUpdateDto category)
        {
            try
            {
                var categoryDetails = this.mapper.Map<Category>(category);
    
                this.database.Categories.Attach(categoryDetails);
    
                this.database.Entry(categoryDetails).State = EntityState.Modified;
    
                await this.database.SaveChangesAsync();
                logger.Information("Category Updated");
    
                return HttpStatusCode.OK;
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }

        public async Task<object> GetAllCategory()
        {
            try
            {
                var existedData = await this.database.Categories.ToListAsync();

                if (existedData != null)
                {
                    var categoryDto = this.mapper.Map<IEnumerable<CategoryUpdateDto>>(existedData);
                    logger.Information("Category Fetched from Server");
                    return categoryDto;
                }
                logger.Information("No Category Found");
                return HttpStatusCode.NotFound;
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception error)
            {
                logger.Error(error , error.Message);
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}