using MediatR;
namespace Playstore.Providers.Handlers.Commands
{
    public class ChangePasswordQuery : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string? Password { get; set; }

    }
}