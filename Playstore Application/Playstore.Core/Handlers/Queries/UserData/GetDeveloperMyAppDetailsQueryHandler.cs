using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetDeveloperMyAppDetails : IRequest<object>
    {
        public Guid _UserId{get;set;}
        public GetDeveloperMyAppDetails(Guid Userid)
        {
            _UserId=Userid;
        }
    }

    public class GetDeveloperMyAppDetailsQueryHandler : IRequestHandler<GetDeveloperMyAppDetails,object>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetDeveloperMyAppDetailsQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDeveloperMyAppDetails request, CancellationToken cancellationToken)
        {
            
            var app = await _repository.MyAppDetails.GetAppDetails(request._UserId);

            if (app == null)
            {
                throw new EntityNotFoundException($"No App found");
            }

            return app;
        }
    }
}