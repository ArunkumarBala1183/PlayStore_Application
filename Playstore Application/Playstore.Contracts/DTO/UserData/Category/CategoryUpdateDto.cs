using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.SignalR;

namespace Playstore.Contracts.DTO.Category
{
    public class CategoryUpdateDto
    {
        public Guid CategoryId{get;set;}
        public string CategoryName{get;set;}
    }
}