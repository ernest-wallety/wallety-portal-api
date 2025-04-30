using MediatR;
using Wallety.Portal.Application.Response.Auth;

namespace Wallety.Portal.Application.Commands
{
    public class CreateLoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
