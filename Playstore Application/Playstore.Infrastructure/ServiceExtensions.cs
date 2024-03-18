using Playstore.Contracts.Data;
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
using Playstore.Core.Data.Repositories.StatusCode;
using Playstore.Providers.Handlers.Commands;
using FluentValidation;
using Playstore.Contracts.DTO;
using Playstore.Core.Validators;
using MediatR;
using Playstore.Providers.Handlers.Queries;
using Playstore.Contracts.Data.Repositories.Admin;
using Playstore.Infrastructure.Data.Repositories;
using Playstore.Infrastructure.Data;
using Playstore.Migrations.Scaffold;

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
            .AddTransient<IAppDataRepository , AppDataRepository>()
            .AddScoped<IStatusCodeHandlerRepository , StatusCodeHandlerRepository>()
            .AddScoped<IUsersRepository, UsersRepository>()
            .AddTransient<RegisterUsersCommandHandler>()
            .AddSingleton<SharedDataService>()
            .AddTransient<IEmailService, EmailService>()
            .AddTransient<IRequestHandler<CheckEmailExistenceQuery, bool>, CheckEmailExistenceQueryHandler>()
            .AddTransient<IRoleRepository, RoleRepository>()
            .AddTransient<IUserRoleRepository, UserRoleRepository>()
            .AddTransient<IDeveloperRole , DeveloperRoleRepository>()
            .AddScoped<IUserCredentialsRepository, UserCredentialsRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddTransient<IApplicationLogsRepository , ApplicationLogsRepository>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSqlServer<DatabaseContext>(configuration.GetConnectionString("SqlServerConnection"), options =>
            {
                options.MigrationsAssembly("Playstore.Migrations");
                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            })
            .AddSqlServer<LogDbContext>(configuration.GetConnectionString("SqlServerConnection"))
            .Configure<RoleConfig>(configuration.GetSection("RoleConfig"));
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
            services.AddScoped<IPasswordHasher<UserCredentials>, PasswordHasher<UserCredentials>>();
            services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }
    }
}