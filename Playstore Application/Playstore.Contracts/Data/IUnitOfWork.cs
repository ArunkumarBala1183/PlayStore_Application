using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.Data.Repositories.Admin;

namespace Playstore.Contracts.Data
{
    public interface IUnitOfWork
    {
       
        IUserRepository User { get; }
        IUserDetailsRepository UserData{get;}
        
        IDeveloperRole UserRole {get;}
        IAppRepository App{get;}
        IAppReviewRepository AppReview{get;}
        IAppValueRepository AppValue{get;}
        IAppInfoDownloadRepository AppDownload{get;}
        IAppImagesRepository AppImages{get;}
        IUserCredentialsRepository UserCredentials{get;}
        IAppFilesRepository AppFiles{get;}
        IAppAdminRequestRepository AdminRequest{get;}
        IAppDeveloperMyAppDetailsRepository MyAppDetails{get;}
        IAppDetailsRepository AppDetails{get;}
        IGetCategory GetCategory{get;}
        Task CommitAsync();
    }
}