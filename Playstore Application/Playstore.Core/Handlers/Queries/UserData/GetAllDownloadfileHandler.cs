using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Utility;


namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAllDownloadfile : IRequest<AppImages>
    {

    }

    public class GetAllDownloadfileHandler : IRequestHandler<GetAllDownloadfile, AppImages>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllDownloadfileHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AppImages> Handle(GetAllDownloadfile request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.AppValue.GetAll());
            
            if(entities==null)
            {
                 throw new EntityNotFoundException(Dataconstant.EntityNotFoundException);
            }
            return _mapper.Map<AppImages>(entities);
        }


    }
}