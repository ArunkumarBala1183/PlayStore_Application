using MediatR;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using FluentValidation;
using System.Text.Json;
using Playstore.Core.Exceptions;
using System;
using System.IO.Pipelines;
using System.Net;

namespace Playstore.Providers.Handlers.Commands.UserData
{
    public class CreateAdminRequestCommand : IRequest<object>
    {
        public Guid Id { get; }
        public CreateAdminRequestCommand(Guid _Id)
        {
            Id = _Id;

        }

    }

    public class CreateAdminRequestCommandHandler : IRequestHandler<CreateAdminRequestCommand, object>
    {
        private readonly IUnitOfWork _repository;



        public CreateAdminRequestCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;

        }




        public async Task<object> Handle(CreateAdminRequestCommand request, CancellationToken cancellationToken)
        {


            return await _repository.AdminRequest.AddRequest(request.Id);


        }

    }
}





