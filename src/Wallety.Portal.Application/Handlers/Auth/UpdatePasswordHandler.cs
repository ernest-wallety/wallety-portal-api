using MediatR;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Enum;
using Wallety.Portal.Core.Helpers;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Requests.User;
using Wallety.Portal.Core.Utils;

namespace Wallety.Portal.Application.Handlers.Auth
{
    public class UpdatePasswordHandler(IUserRepository repository) :
        IRequestHandler<UpdateCommand<string, UpdateResponse>, UpdateResponse>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<UpdateResponse> Handle(UpdateCommand<string, UpdateResponse> request, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetUserByEmail(request.Item) ?? throw new KeyNotFoundException("User does not exist.").WithDisplayData(EnumValidationDisplay.Toastr);

            var result = await _repository.UpdatePassword(
                    new PasswordResetModel()
                    {
                        UserId = existingUser.UserId,
                        OneTimePasswordGuid = Guid.NewGuid(),
                        Email = request.Item,
                        OldPassword = existingUser.PasswordHash,
                        // SET THE DESIRED PASSWORD
                        NewPassword = CryptoUtil.HashMultiple("", existingUser.SecurityStamp),
                        Salt = existingUser.SecurityStamp
                    });

            return LazyMapper.Mapper.Map<UpdateResponse>(result);
        }
    }
}
