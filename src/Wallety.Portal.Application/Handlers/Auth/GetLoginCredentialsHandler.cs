using MediatR;
using Wallety.Portal.Application.Queries;
using Wallety.Portal.Application.Response.Auth;
using Wallety.Portal.Core.Services;

namespace Wallety.Portal.Application.Handlers.Auth
{
    public class GetLoginCredentialsHandler(
         ICachingInMemoryService caching) :
         IRequestHandler<GetLoginCredentialsQuery,
         LoginResponse>
    {
        private readonly ICachingInMemoryService _caching = caching;

        public async Task<LoginResponse> Handle(GetLoginCredentialsQuery request, CancellationToken cancellationToken)
        {
            var token = _caching.Get<string>("Token");

            return _caching.Get<LoginResponse>(token)
                ?? throw new ArgumentException("The provided session token is invalid.");
        }
    }
}
