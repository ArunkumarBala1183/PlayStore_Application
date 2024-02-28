using MediatR;
using Playstore.Contracts.Data;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using FluentValidation;
using System.Text.Json;
using Playstore.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Playstore.Core.Validators;
using Playstore.Contracts.Data.Repositories;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Playstore.Providers.Handlers.Commands
{
    
    public class RegisterUsersCommandHandler : IRequestHandler<RegisterUsersCommand, Guid>
    {
        private readonly IUsersRepository _repository;
        private readonly IUserCredentialsRepository _repository1;
        private readonly IValidator<RegisterUsersDTO> _validator;
        private readonly IPasswordHasher<UserCredentials> _passwordHasher;
        private readonly IMapper mapper;
        private readonly IEmailService _emailService;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public RegisterUsersCommandHandler(IMapper mapper,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IEmailService emailService,
        IUsersRepository repository, IUserCredentialsRepository repository1,
        IValidator<RegisterUsersDTO> validator,
        IPasswordHasher<UserCredentials> passwordHasher)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _repository1 = repository1;
            _validator = validator;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _userRoleRepository = userRoleRepository;
            this.mapper = mapper;
        }
        public async Task<Guid> Handle(RegisterUsersCommand request, CancellationToken cancellationToken)
        {
            RegisterUsersDTO model = request.Model;


            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var userEntity = new Users
            {
                Name = model.Name,
                EmailId = model.EmailId,
                DateOfBirth = DateOnly.FromDateTime(model.DateOfBirth),
                MobileNumber = model.MobileNumber
            };
            //var userEntity = this.mapper.Map<Users>(model);
            var existingMobileNumber = await _repository.GetByPhoneNumber(model.MobileNumber);
            var existingUserInUsers = await _repository.GetByEmailId(model.EmailId);

            if (existingUserInUsers != null)
            {
                throw new DuplicateEmailException("Email is already registered.");
            }
            if (existingMobileNumber != null)
            {
                throw new DuplicateEmailException("MobileNumber is already registered.");
            }
            //var userEntity = this.mapper.Map<Users>(model);
            _repository.Add(userEntity);
            await _repository.CommitAsync();
            await this.GenerateUserCredentials(userEntity.EmailId, request);
            Guid defaultRoleId = await _roleRepository.GetDefaultRoleId();
            await this.GenerateUserRole(userEntity, defaultRoleId);
            await _emailService.SendUserCredentialsAsync(model.EmailId, model.Name, model.MobileNumber, DateOnly.FromDateTime(model.DateOfBirth));
            return userEntity.UserId;
        }
        private async Task GenerateUserRole(Users userEntity, Guid defaultRoleId)
        {
            var defaultRole = await _roleRepository.GetByRoleId(defaultRoleId);
            if (defaultRole != null)
            {
                var userRole = new UserRole
                {
                    UserId = userEntity.UserId,
                    RoleId = defaultRole.RoleId
                };

                _userRoleRepository.Add(userRole);
                await _userRoleRepository.CommitAsync();
            }
        }


        private async Task GenerateUserCredentials(string emailId, RegisterUsersCommand request)
        {
            var userDetails = await _repository.GetByEmailId(emailId);



            if (userDetails != null)
            {

                var userCredentialsEntity = new UserCredentials
                {
                    //Password = userCredentialsEntity.Password,
                    EmailId = userDetails.EmailId,
                    UserId = userDetails.UserId
                };
                userCredentialsEntity.Password = _passwordHasher.HashPassword(userCredentialsEntity, request.Model.Password);


                _repository1.Add(userCredentialsEntity);
                await _repository1.CommitAsync();

                //await SendEmail(userCredentials, userDetails.EmailId);


            }
        }
    }
}

