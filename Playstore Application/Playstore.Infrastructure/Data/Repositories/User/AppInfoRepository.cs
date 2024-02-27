
// using Playstore.Contracts.Data.Repositories;
// using Playstore.Migrations;
// using System.Net;
// using Microsoft.EntityFrameworkCore;
// // using Playstore.Contracts.DTO;
// using AutoMapper;
// using Playstore.Contracts.Data.Entities;
// using Playstore.Contracts.DTO;
// using Playstore.Core.Exceptions;
// using Playstore.Infrastructure.Data.Repositories.Generic;

// namespace Playstore.Infrastructure.Data.Repositories
// {
//     public class AppInfoRepository :Repository<AppInfo>, IAppInfoRepos
//     {
//         private readonly DatabaseContext _database;

//         public AppInfoRepository(DatabaseContext context) : base(context)
//         {
//             _database=context;
//         }

//         public async Task<HttpStatusCode> RemoveApp(Guid id)
//         {
//             var existedData = await this._database.AppInfo.FindAsync(id);

//             if(existedData != null)
//             {
//                 this._database.AppInfo.Remove(existedData);

//                 await this._database.SaveChangesAsync();
//             }

//             return HttpStatusCode.NoContent;
//         }

       
       
//     }
// }