
using MediatR;
using Playstore.Contracts.DTO;

namespace Playstore.Providers.Handlers.Commands
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class LoginUsersCommand : IRequest<TokenResponse>
    {
        public LoginUsersDTO Model { get; }

        public LoginUsersCommand(LoginUsersDTO model)
        {
            this.Model = model;
        }
    }
}