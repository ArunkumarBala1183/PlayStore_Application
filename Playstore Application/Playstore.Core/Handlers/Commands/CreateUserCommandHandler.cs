using MediatR;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using FluentValidation;
using System.Text.Json;
using Playstore.Core.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Playstore.Providers.Handlers.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserDTO Model { get; }
        public CreateUserCommand(CreateUserDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateUserDTO> _validator;
        private readonly IPasswordHasher<User> _passwordHasher;

        public CreateUserCommandHandler(IUnitOfWork repository, IValidator<CreateUserDTO> validator, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _validator = validator;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            CreateUserDTO model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }


            var entity = new User
            {
                Username = model.Username,

            };
             entity.Password= _passwordHasher.HashPassword(entity, model.Password);
            _repository.User.Add(entity);
            await _repository.CommitAsync();

            return entity.Id;
        }
    }
}