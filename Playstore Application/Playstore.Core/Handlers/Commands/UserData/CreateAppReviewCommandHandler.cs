using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playstore.Contracts.DTO.AppReview;
using Playstore.Core.Exceptions;
using Playstore.Contracts.Data.Utility;

namespace Playstore.Providers.Handlers.Commands.UserData;
public class CreateAppReviewCommand : IRequest<Guid>
{
   public AppreviewDTO Model { get; set; }
   public CreateAppReviewCommand(AppreviewDTO model)
   {
      Model = model;
   }

}
public class CreateAppReviewCommandHandler : IRequestHandler<CreateAppReviewCommand, Guid>
{
   public IUnitOfWork _repository;
   public CreateAppReviewCommandHandler(IUnitOfWork unitOfWork)
   {
      _repository = unitOfWork;
   }

   public async Task<Guid> Handle(CreateAppReviewCommand request, CancellationToken cancellationToken)
   {
      if(request!=null){
      AppreviewDTO model = request.Model;

      var entity = new AppReview
      {
         AppId = model.AppId,
         UserId = model.UserId,
         Rating = model.Rating,
         Comment = model.Commands
      };
      _repository.AppReview.Add(entity);
      await _repository.CommitAsync();
      return entity.Id;
      }
      
      throw new ObjectNullException(Dataconstant.ObjectNullException);
     
   }
}