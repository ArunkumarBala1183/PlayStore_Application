using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Repositories.Admin;
using Playstore.Contracts.Data.RoleConfig;
using Playstore.Core.Data.Repositories;
using Playstore.Core.Data.Repositories.Admin;
using Playstore.Infrastructure.Data.Repositories;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        private readonly IOptions<RoleConfig> roleConfig;
        private readonly IHttpContextAccessor httpContext;

        public UnitOfWork(DatabaseContext context , IOptions<RoleConfig> roleConfig , IHttpContextAccessor httpContext)
        {
            _context = context;
            this.roleConfig = roleConfig;
            this.httpContext = httpContext;
        }
        public IAppRepository App => new AppRepository(_context);

        

        public IDeveloperRole UserRole => new DeveloperRoleRepository(_context , roleConfig , httpContext);
        
        
        public IAppValueRepository AppValue => new AppValueRepository(_context , httpContext);
        public IAppReviewRepository AppReview => new AppReviewRepository(_context , httpContext);
        public IAppFileRepository AppData=>new AppFileRepository(_context);
        public IAppInfoDownloadRepository AppDownload=>new AppInfoDownloadRepository(_context , httpContext);
        public IAppImagesRepository AppImages=>new AppImagesRepository(_context);
        public IAppFilesRepository AppFiles=>new AppFilesRepository(_context , roleConfig , httpContext);
        public IAppAdminRequestRepository AdminRequest=>new AppAdminRequestRepository(_context , httpContext);
        public IAppDeveloperMyAppDetailsRepository MyAppDetails=> new AddDeveloperMyAppDetailsRespository(_context , httpContext);
        public IAppDetailsRepository AppDetails=>new AppDetailsRepository(_context , httpContext);

        public IGetCategory GetCategory=>new GetCategoryRepository(_context);
        public IUserCredentialsRepository UserCredentials=>new UserCredentialsRepository(_context , httpContext);

        

        public IUserDetailsRepository UserData=>new GetUsersDetailsRepository(_context , httpContext);

        public IUserRepository User => throw new NotImplementedException();

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}