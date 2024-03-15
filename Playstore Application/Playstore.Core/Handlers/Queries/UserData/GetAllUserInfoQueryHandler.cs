using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using MediatR;
using System.Linq;
using Playstore.Contracts.DTO.AppReview;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    
    public class GetAllUserInfoQuery : IRequest<UsersDetailsDTO>
    {
        
        public Guid UserId {get;set;}
        public GetAllUserInfoQuery(Guid userId)
        {
          

            UserId = userId;

        }
    }

    public class GetAllUserInfoQueryHandler : IRequestHandler<GetAllUserInfoQuery,UsersDetailsDTO>
    {
        private readonly IUnitOfWork _repository;
       

        public GetAllUserInfoQueryHandler(IUnitOfWork repository)
        {
            _repository = repository;
         
        }

        public async Task<UsersDetailsDTO> Handle(GetAllUserInfoQuery request, CancellationToken cancellationToken)
        {
            if(request!=null){
            var app = await _repository.UserData.GetUsersDetails(request.UserId);
            return (UsersDetailsDTO)app;
            }
           
                throw new ObjectNullException($"No Data Found");
           
        }
    }
}