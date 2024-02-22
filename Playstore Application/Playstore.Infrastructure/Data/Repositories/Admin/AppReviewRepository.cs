using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppReview;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories.Admin
{
    public class AppReviewRepository : IAppReviewRepository
    {
        private readonly DatabaseContext database;

        private readonly IMapper mapper;

        public AppReviewRepository(DatabaseContext database , IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }
        public async Task<object> AddReview(Guid appReviewDetails)
        {
           try
           {
              
            var appInfoDetails = await this.database.AppInfo.FindAsync(appReviewDetails);

              if(appInfoDetails == null)
              {
                return HttpStatusCode.NotFound;
              }

            //   var reviewDetails = this.mapper.Map<AppReview>(appReviewDetails);
              
            //   appInfoDetails.AppReview.Add(reviewDetails);
              
            //   await this.database.SaveChangesAsync();

            return appInfoDetails.AppId;

           }
           catch (Exception error)
           {
                Console.WriteLine(error.Message);
                return new Guid();
           }
        }
    }
}