using MediatR;
using Wallety.Portal.Application.Response.Auth;

namespace Wallety.Portal.Application.Commands
{
    public class CreateRegisterCommand : IRequest<RegisterResponse>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
