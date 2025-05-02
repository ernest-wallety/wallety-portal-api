using MediatR;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Dto.User;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Requests.User;
using Wallety.Portal.Core.Results;
using Wallety.Portal.Core.Templates;
using Wallety.Portal.Core.Utils;

namespace Wallety.Portal.Application.Handlers.Auth
{
    public class OneTimePasswordHandler(
        IUserRepository userRepository,
        IOutboundMailRepository mailRepository) :
        IRequestHandler<UpdateCommand<PasswordResetDTO, UpdateResponse>, UpdateResponse>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IOutboundMailRepository _mailRepository = mailRepository;

        public async Task<UpdateResponse> Handle(UpdateCommand<PasswordResetDTO, UpdateResponse> request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByEmail() ?? throw new KeyNotFoundException("User does not exist.");

            var response = new UpdateRecordResult();

            if (!request.Item.IsNewUser)
            {
                response = await _userRepository.UpdatePassword(
                    new PasswordResetModel()
                    {
                        UserId = existingUser.UserId,
                        OneTimePasswordGuid = Guid.NewGuid(),
                        Email = request.Item.Email,
                        OldPassword = existingUser.PasswordHash,
                        NewPassword = CryptoUtil.HashMultiple(request.Item.NewPassword!, existingUser.SecurityStamp),
                        Salt = existingUser.SecurityStamp
                    });
            }

            var mailResponse = await _mailRepository.CreateMessageLogRecord(
               new MessageLogEntity
               {
                   Subject = "Reset Password",
                   ToField = request.Item.Email!,
                   Body = PasswordGeneratorTemplate.GenerateHTML(request.Item.Email!, request.Item.NewPassword!),
                   FromName = "Wallety"
               });

            if (mailResponse.IsSuccess)
                response = UpdateRecordResult.Successs("Password successfully reset!");

            return LazyMapper.Mapper.Map<UpdateResponse>(response);
        }
    }
}
