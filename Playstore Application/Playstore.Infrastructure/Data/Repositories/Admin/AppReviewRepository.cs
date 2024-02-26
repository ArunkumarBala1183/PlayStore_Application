using System.Net;
using AutoMapper;
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

    public AppReviewRepository(DatabaseContext database, IMapper mapper)
    {
      this.database = database;
      this.mapper = mapper;
    }
    public async Task<object> AddReview(AppReviewDto appReviewDetails)
    {
      try
      {
        var reviewDetails = this.mapper.Map<AppReview>(appReviewDetails);

        await this.database.AppReviews.AddAsync(reviewDetails);

        await this.database.SaveChangesAsync();

        return HttpStatusCode.Created;

      }
      catch (Exception)
      {
        return HttpStatusCode.InternalServerError;
      }
    }
  }
}