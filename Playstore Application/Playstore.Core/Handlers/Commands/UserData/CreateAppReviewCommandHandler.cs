using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playstore.Contracts.DTO.AppReview;

namespace Playstore.Providers.Handlers.Commands.UserData;
public class CreateAppReviewCommand:IRequest<Guid>
{
   public AppreviewDTO Model{get;set;}
   public CreateAppReviewCommand(AppreviewDTO model)
   {
    Model=model;
   }

}
public class CreateAppReviewCommandHandler : IRequestHandler<CreateAppReviewCommand,Guid>
{
   public IUnitOfWork _repository;
   public CreateAppReviewCommandHandler(IUnitOfWork unitOfWork)
   {
      _repository=unitOfWork;
   }

    public async Task<Guid> Handle(CreateAppReviewCommand request, CancellationToken cancellationToken)
    {
        AppreviewDTO model=request.Model;
      //  Console.WriteLine(model.AppId);
      //  await _repository.CommitAsync();
        var entity = new AppReview
        {
          AppId=model.AppId,
          UserId=model.UserId,
          Rating=model.Rating,
          Comment=model.Commands
          };         
        _repository.AppReview.Add(entity);
        await _repository.CommitAsync();
        return entity.Id;
    }
}