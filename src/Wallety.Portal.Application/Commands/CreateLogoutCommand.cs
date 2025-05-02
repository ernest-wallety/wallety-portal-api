using MediatR;
using Wallety.Portal.Application.Response.General;

namespace Wallety.Portal.Application.Commands
{
    public class CreateLogoutCommand : IRequest<UpdateResponse> { }
}
