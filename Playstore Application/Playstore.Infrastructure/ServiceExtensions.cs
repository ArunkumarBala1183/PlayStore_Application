using Playstore.Contracts.Data;
using Playstore.Core.Data;
using Playstore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data.Entities;

namespace Playstore.Infrastructure
{
    public static class ServiceExtensions
    {
        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSqlite<DatabaseContext>(configuration.GetConnectionString("DefaultConnection"), (options) =>
            {
                options.MigrationsAssembly("Playstore.Migrations");
            });
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }
    }
}
