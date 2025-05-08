using System.Text.Json;
using MediatR;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Dto.Customer;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Enum;
using Wallety.Portal.Core.Helpers;
using Wallety.Portal.Core.Helpers.Constants;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Requests.Customer;
using Wallety.Portal.Core.Templates.Wati;

namespace Wallety.Portal.Application.Handlers.Customer
{
    public class VerifyCustomerAccountHandler(
        IUserRepository userRepository,
        ICustomerRepository customerRepository,
        IOutboundMailRepository mailRepository) :
        IRequestHandler<CreateCommand<CustomerVerificationDTO, CreateResponse>, CreateResponse>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IOutboundMailRepository _mailRepository = mailRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<CreateResponse> Handle(CreateCommand<CustomerVerificationDTO, CreateResponse> request, CancellationToken cancellationToken)
        {
            var dto = new CustomerVerificationModel
            {
                CustomerId = EncryptionHelper.Decrypt(request.Item.CustomerId!),
                RegistrationStatusId = EncryptionHelper.Decrypt(request.Item.RegistrationStatusId!),
                VerificationRejectReasonId = EncryptionHelper.Decrypt(request.Item.VerificationRejectReasonId!)
            };

            var customerAccount = await _userRepository.GetUserById(Guid.Parse(dto.CustomerId!))
                ?? throw new Exception("Customer account does not exist!")
                    .WithDisplayData(EnumValidationDisplay.Toastr);

            if (Guid.Parse(dto.RegistrationStatusId) == RegistrationStatusConstants.REJECTED)
                await HandleRejectedCustomerAsync(dto, customerAccount);
            else
                await SendSignUpMessageAsync(customerAccount);

            var response = await _customerRepository.VerifyCustomerAccount(dto);
            if (!response.IsSuccess)
                throw new ArgumentException(response.ResponseMessage).WithDisplayData(EnumValidationDisplay.Toastr);

            return LazyMapper.Mapper.Map<CreateResponse>(response);
        }

        private async Task HandleRejectedCustomerAsync(CustomerVerificationModel dto, UserEntity customerAccount)
        {
            var rejectReason = (await _customerRepository.GetVerificationRejectReasons())
                .FirstOrDefault(x => x.RejectReasonId == Guid.Parse(dto.VerificationRejectReasonId!));

            if (string.IsNullOrEmpty(rejectReason!.RejectMessage))
                throw new Exception("Reject reason not found.")
                    .WithDisplayData(EnumValidationDisplay.Toastr);

            await SendMessageWithCheckAsync(new MessageLogEntity
            {
                MessageLogTypeId = (await _mailRepository.GetMessageLogTypes())
                    .FirstOrDefault(x => x.MessageLogTypeCode == MessageTypeLogConstants.WATI)!
                    .MessageLogTypeId,
                Subject = "Wati Service",
                ToField = customerAccount.PhoneNumber,
                Body = PayloadTemplates.SendConfirmationMessage(rejectReason.RejectMessage)
            });

            await SendMessageWithCheckAsync(new MessageLogEntity
            {
                MessageLogTypeId = (await _mailRepository.GetMessageLogTypes())
                    .FirstOrDefault(x => x.MessageLogTypeCode == MessageTypeLogConstants.WATI)!
                    .MessageLogTypeId,
                Subject = "Wati Service",
                ToField = customerAccount.PhoneNumber,
                Body = PayloadTemplates.SendEditAccountDetailsTemplate(customerAccount.UserId)
            });
        }

        private async Task SendSignUpMessageAsync(UserEntity customerAccount)
        {
            await _mailRepository.CreateMessageLogRecord(new MessageLogEntity
            {
                MessageLogTypeId = (await _mailRepository.GetMessageLogTypes())
                    .FirstOrDefault(x => x.MessageLogTypeCode == MessageTypeLogConstants.WATI)!
                    .MessageLogTypeId,
                Subject = "Wati Service",
                ToField = customerAccount.PhoneNumber,
                Body = PayloadTemplates.SendSignUpMessage(customerAccount.FirstName, customerAccount.Surname)
            });
        }

        private async Task SendMessageWithCheckAsync(MessageLogEntity message)
        {
            var result = await _mailRepository.CreateMessageLogRecord(message);

            if (!result.IsSuccess) throw new Exception(result.ResponseMessage).WithDisplayData(EnumValidationDisplay.Toastr);
        }
    }
}
