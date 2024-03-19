using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Contracts.Data.Utility;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetDeveloperMyAppDetails : IRequest<IEnumerable<Myappdetails>>
    {
        public Guid UserId { get; set; }
        public GetDeveloperMyAppDetails(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetDeveloperMyAppDetailsQueryHandler : IRequestHandler<GetDeveloperMyAppDetails, IEnumerable<Myappdetails>>
    {
        private readonly IUnitOfWork _repository;

        public GetDeveloperMyAppDetailsQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Myappdetails>> Handle(GetDeveloperMyAppDetails request, CancellationToken cancellationToken)
        {
            if(request!=null){
            var app = await _repository.MyAppDetails.GetAppDetails(request.UserId);

            if (app == null)
            {
                throw new EntityNotFoundException(Dataconstant.EntityNotFoundException);
            }

            return (IEnumerable<Myappdetails>)app;
            }
           
            throw new ObjectNullException(Dataconstant.ObjectNullException);
        }
    }
}