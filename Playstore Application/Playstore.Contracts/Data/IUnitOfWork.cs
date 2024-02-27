using Playstore.Contracts.Data.Repositories;

namespace Playstore.Contracts.Data
{
    public interface IUnitOfWork
    {
       
        IUserRepository User { get; }
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
         
        Task CommitAsync();
    }
}