using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Repositories.Admin;

namespace Playstore.Contracts.Data
{
    public interface IUnitOfWork
    {
       
        IUserRepository User { get; }

        IDeveloperRole UserRole {get;}
        
        IAppRepository App{get;}
        IAppReviewRepository AppReview{get;}
        IAppDataRepository AppData{get;}
        IAppValueRepository AppValue{get;}
        IAppInfoDownloadRepository AppDownload{get;}
        IAppImagesRepository AppImages{get;}
        IAppFilesRepository AppFiles{get;}
        IAppAdminRequestRepository AdminRequest{get;}
        IAppDeveloperMyAppDetailsRepository MyAppDetails{get;}
        IAppInfoRepository AppInfo{get;}
        IAppDetailsRepository AppDetails{get;}
        IGetCategory GetCategory{get;}
        ICategoryRepository CategoryDetails {get;}
        Task CommitAsync();
    }
}