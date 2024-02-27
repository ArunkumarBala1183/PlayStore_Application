﻿using Playstore.Contracts.Data;
using Playstore.Core.Data;
using Playstore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Data.Repositories;
using Playstore.Core.Data.Repositories.Admin;
using Playstore.Contracts.Data.RoleConfig;

namespace Playstore.Infrastructure
{
    public static class ServiceExtensions
    {
        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddTransient<ICategoryRepository , CategoryRepository>()
            .AddTransient<IAppInfoRepository , AppInfoRespository>()
            .AddTransient<IUserInfoRepository , UserInfoRepository>()
            .AddTransient<IAppDownloadsRepository , AppDownloadsRepository>()
            .AddTransient<IAppReviewRepository , AppReviewRepository>()
            .AddTransient<IAppRequestsRepository , AppRequestsRepository>()
            .AddTransient<IAppDataRepository , AppDataRepository>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSqlServer<DatabaseContext>(configuration.GetConnectionString("SqlServerConnection"), (options) =>
            {
                options.MigrationsAssembly("Playstore.Migrations");
            })
            .Configure<RoleConfig>(configuration.GetSection("RoleConfig"));
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }
    }
}
