using System.Net;
using MediatR;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Commands
{
    public class AddCategoryCommand : IRequest<HttpStatusCode>
    {
        public CategoryDto Category { get; set; }
        public AddCategoryCommand(CategoryDto category)
        {
            this.Category = category;
        }  
    }   
}