using MediatR;
using Wallety.Portal.Application.Response.Auth;

namespace Wallety.Portal.Application.Queries
{
    public class GetLoginCredentialsQuery : IRequest<LoginResponse>
    {
        public string SessionToken { get; set; }
    }
}
