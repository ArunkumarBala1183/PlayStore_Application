using Playstore.Contracts.Data;
using Playstore.Core.Data;
using Playstore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Data.Repositories;
using Playstore.Providers.Handlers.Commands;
using FluentValidation;
using Playstore.Contracts.DTO;
using Playstore.Core.Validators;
using MediatR;
using Playstore.Providers.Handlers.Queries;

namespace Playstore.Infrastructure
{
    public static class ServiceExtensions
    {
        private static IServiceCollection AddUsersRepository(this IServiceCollection services)
        {
            return services.AddScoped<IUsersRepository, UsersRepository>();
            
        }
        private static IServiceCollection RegisterUsersCommandHandler(this IServiceCollection services)
        {
            return services.AddTransient<RegisterUsersCommandHandler>();
            
        }
        private static IServiceCollection EmailService(this IServiceCollection services)
        {
            return services.AddTransient<IEmailService, EmailService>();
            
        }

        
        private static IServiceCollection CheckEmailExistenceQuery(this IServiceCollection services)
        {
            return services.AddTransient<IRequestHandler<CheckEmailExistenceQuery, bool>, CheckEmailExistenceQueryHandler>();
            
        }
        private static IServiceCollection RoleService(this IServiceCollection services)
        {
            return services.AddTransient<IRoleRepository, RoleRepository>();
            
        }
        private static IServiceCollection UserRoleService(this IServiceCollection services)
        {
            return services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            
        }
        private static IServiceCollection PasswordReset(this IServiceCollection services)
        {
            return services.AddTransient<IValidator<PasswordResetDTO>, PasswordResetDTOValidator>();

            
        }
        private static IServiceCollection PasswordValidator(this IServiceCollection services)
        {
            return services.AddValidatorsFromAssemblyContaining<PasswordResetDTOValidator>();

            
        }

        
        private static IServiceCollection AddUserCredentialsRepository(this IServiceCollection services)
        {
            return services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();
        }

        private static IServiceCollection RefreshTokenRepository(this IServiceCollection services)
        {
            return services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSqlServer<DatabaseContext>(configuration.GetConnectionString("SqlServerConnection"), (options) =>
            {
                options.MigrationsAssembly("Playstore.Migrations");
            });
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
            services.AddScoped<IPasswordHasher<UserCredentials>, PasswordHasher<UserCredentials>>();
            return services.AddDatabaseContext(configuration).AddUnitOfWork()
                           .EmailService().RegisterUsersCommandHandler()
                           .AddUserCredentialsRepository().AddUsersRepository()
                           .UserRoleService().RoleService().RefreshTokenRepository()
                           .CheckEmailExistenceQuery().PasswordReset().PasswordValidator();
        }
    }
}
