using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Migrations;
 
namespace Playstore.Core.Data.Repositories
{
    public class SharedDataService
    {
        public string? ResetPasswordEmail { get; set; }
 
        public string? ResetPasswordOTP { get; set; }
    }
}