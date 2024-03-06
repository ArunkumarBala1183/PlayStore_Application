using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Repositories;
// using Playstore.Core.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories;
using Playstore.Migrations;

namespace Playstore.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public IAppRepository App => new AppRepository(_context);

        public IUserRepository User => new UserRepository(_context);
        
        
        public IAppValueRepository AppValue => new AppInfoRespository(_context);
        public IAppReviewRepository AppReview => new AppReviewRepository(_context);
        public IAppDataRepository AppData=>new AppDataRepository(_context);
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