using MediatR;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Dto.User;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Enum;
using Wallety.Portal.Core.Helpers;
using Wallety.Portal.Core.Helpers.Constants;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Results;
using Wallety.Portal.Core.Templates;
using Wallety.Portal.Core.Utils;

namespace Wallety.Portal.Application.Handlers.Users
{
    public class CreateUserHandler(
        IUserRepository userRepository,
        IOutboundMailRepository mailRepository) :
        IRequestHandler<CreateCommand<UserRegistrationDTO, CreateResponse>, CreateResponse>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IOutboundMailRepository _mailRepository = mailRepository;

        public async Task<CreateResponse> Handle(CreateCommand<UserRegistrationDTO, CreateResponse> request, CancellationToken cancellationToken)
        {
            var securityStamp = CryptoUtil.GenerateSalt();
            var newPassword = PasswordUtil.GeneratePassword();
            var passwordHash = CryptoUtil.HashMultiple(newPassword, securityStamp);

            var newUser = new UserEntity
            {
                FirstName = request.Item.Name,
                Surname = request.Item.Surname,
                PhoneNumber = request.Item.WhatsappNumber,
                PhoneNumberConfirmed = true,
                Username = request.Item.Email,
                Email = request.Item.Email,
                PasswordHash = passwordHash,
                SecurityStamp = securityStamp,
                RoleId = request.Item.RoleId,
            };

            var result = (request.Item != null) ? await _userRepository.CreateUser(newUser) : new CreateRecordResult();

            if (!result.IsSuccess) throw new Exception(result.ResponseMessage).WithDisplayData(EnumValidationDisplay.Toastr);

            var mailResponse = await _mailRepository.CreateMessageLogRecord(
                new MessageLogEntity
                {
                    MessageLogTypeId = (await _mailRepository.GetMessageLogTypes())
                    .FirstOrDefault(x => x.MessageLogTypeCode == MessageTypeLogConstants.WATI)!
                    .MessageLogTypeId,
                    Subject = "Reset Password",
                    ToField = request.Item?.Email!,
                    Body = PasswordGeneratorTemplate.GenerateHTML(request.Item?.Email!, newPassword!),
                    FromName = "Wallety"
                });

            if (!mailResponse.IsSuccess) throw new Exception(mailResponse.ResponseMessage).WithDisplayData(EnumValidationDisplay.Toastr);

            return LazyMapper.Mapper.Map<CreateResponse>(result);
        }
    }
}
