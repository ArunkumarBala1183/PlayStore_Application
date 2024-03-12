using MediatR;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetPasswordQuery : IRequest<bool>
    {
        public string Password;
        public Guid UserId;
        public GetPasswordQuery(Guid UserId, string Password)
        {
            this.Password = Password;
            this.UserId = UserId;
        }   
    }
}