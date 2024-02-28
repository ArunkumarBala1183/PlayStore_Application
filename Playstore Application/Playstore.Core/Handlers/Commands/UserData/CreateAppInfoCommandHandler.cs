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
    public class CreateAppInfoCommand : IRequest<HttpStatusCode>
    {
        public CreateAppInfoDTO Model { get; }
        public CreateAppInfoCommand(CreateAppInfoDTO model)
        {
            Model = model;

        }

    }

    public class CreateAppInfoCommandHandler : IRequestHandler<CreateAppInfoCommand,HttpStatusCode>
    {
        private readonly IUnitOfWork _repository;
        public AppImages AppNumber { get; set; }


        public CreateAppInfoCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
           
        }




        public async Task<HttpStatusCode> Handle(CreateAppInfoCommand request, CancellationToken cancellationToken)
        {
            CreateAppInfoDTO model = request.Model;

            return await _repository.AppFiles.AddFiles(model);           
        }

    }
}





