using Microsoft.Extensions.Options;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Repositories.Admin;
using Playstore.Contracts.Data.RoleConfig;
using Playstore.Core.Data.Repositories;
using Playstore.Core.Data.Repositories.Admin;
// using Playstore.Core.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        private readonly IOptions<RoleConfig> roleConfig;

        public UnitOfWork(DatabaseContext context , IOptions<RoleConfig> roleConfig)
        {
            _context = context;
            this.roleConfig = roleConfig;
        }
        public IAppRepository App => new AppRepository(_context);

        public IUserRepository User => new UserRepository(_context);

        public IDeveloperRole UserRole => new DeveloperRoleRepository(_context , roleConfig);
        
        
        public IAppValueRepository AppValue => new AppValueRepository(_context);
        public IAppReviewRepository AppReview => new AppReviewRepository(_context);
        public IAppFileRepository AppData=>new AppFileRepository(_context);
        public IAppInfoDownloadRepository AppDownload=>new AppInfoDownloadRepository(_context);
        public IAppImagesRepository AppImages=>new AppImagesRepository(_context);
        public IAppFilesRepository AppFiles=>new AppFilesRepository(_context);
        public IAppAdminRequestRepository AdminRequest=>new AppAdminRequestRepository(_context);
        public IAppDeveloperMyAppDetailsRepository MyAppDetails=> new AddDeveloperMyAppDetailsRespository(_context);

        public IAppInfoRepository AppInfo => throw new NotImplementedException();
        public IAppDetailsRepository AppDetails=>new AppDetailsRepository(_context);

        public IGetCategory GetCategory=>new GetCategoryRepository(_context);

        // public IAppInfoRepository AppInfo=new AppInfoRepository();
        // public IAppInfoRepository AppInfo => new AppInfoRepository();

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}