using MediatR;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Results;
using Wallety.Portal.Core.Templates;
using Wallety.Portal.Core.Utils;

namespace Wallety.Portal.Application.Handlers.Users
{
    public class CreateUserHandler(
        IUserRepository userRepository,
        IOutboundMailRepository mailRepository) :
        IRequestHandler<CreateCommand<UserEntity, CreateResponse>, CreateResponse>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IOutboundMailRepository _mailRepository = mailRepository;

        public async Task<CreateResponse> Handle(CreateCommand<UserEntity, CreateResponse> request, CancellationToken cancellationToken)
        {
            var newPassword = PasswordUtil.GeneratePassword();

            request.Item.PasswordHash = CryptoUtil.HashMultiple(newPassword, CryptoUtil.GenerateSalt());

            var result = (request.Item != null) ? await _userRepository.CreateUser(request.Item) : new CreateRecordResult();

            if (result.IsSuccess)
            {
                var mailResponse = await _mailRepository.CreateMessageLogRecord(
                    new MessageLogEntity
                    {
                        Subject = "Reset Password",
                        ToField = request.Item?.Email!,
                        Body = PasswordGeneratorTemplate.GenerateHTML(request.Item?.Email!, newPassword!),
                        FromName = "Wallety"
                    });

                if (!mailResponse.IsSuccess) throw new Exception(mailResponse.ResponseMessage); ;
            }

            return LazyMapper.Mapper.Map<CreateResponse>(result);
        }
    }
}
