
using MediatR;

namespace Playstore.Providers.Handlers.Commands
{
    public class RefreshTokenCommand : IRequest<TokenResponse>
    {
        public string? ExpiredToken { get; set; }
    }
}