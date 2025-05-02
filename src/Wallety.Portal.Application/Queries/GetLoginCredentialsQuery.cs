using MediatR;
using Wallety.Portal.Application.Response.Auth;

namespace Wallety.Portal.Application.Queries
{
    public class GetLoginCredentialsQuery : IRequest<LoginResponse>
    {
        private string? _sessionToken;

        public string SessionToken
        {
            get => _sessionToken ?? string.Empty;
            set => _sessionToken = value.StartsWith("Bearer ") == true
                ? value["Bearer ".Length..].Trim()
                : value;
        }
    }
}
