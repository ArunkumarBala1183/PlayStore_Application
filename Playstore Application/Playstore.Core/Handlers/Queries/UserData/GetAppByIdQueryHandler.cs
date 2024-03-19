using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Utility;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAppByIdValueQuery : IRequest<IEnumerable<AppInfoDetailsDTO>>
    {
        public Guid AppId { get; }
        public Guid UserId{get; }
        public GetAppByIdValueQuery(Guid appId,Guid userId)
        {
            AppId = appId;
            UserId=userId;
        }
    }

    public class GetAppByIdQueryHandler : IRequestHandler<GetAppByIdValueQuery, IEnumerable<AppInfoDetailsDTO>>
    {
        private readonly IUnitOfWork _repository;

        public GetAppByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AppInfoDetailsDTO>> Handle(GetAppByIdValueQuery request, CancellationToken cancellationToken)
        {
            if(request!=null)
            {
            var app = await _repository.AppDetails.GetAppDetails(request.AppId,request.UserId);

            if (app == null)
            {
                throw new EntityNotFoundException(Dataconstant.EntityNotFoundException);
            }

            return (IEnumerable<AppInfoDetailsDTO>)app;
            }
            
                throw new ObjectNullException(Dataconstant.ObjectNullException);
            
        }
    }
}