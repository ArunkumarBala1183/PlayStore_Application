using MediatR;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data.Repositories;
namespace Playstore.Providers.Handlers.Queries;
 public class ChangePasswordQuery : IRequest<string>
    {
        public Guid userId { get; set; }
        public string password{get;set;}
       
    }