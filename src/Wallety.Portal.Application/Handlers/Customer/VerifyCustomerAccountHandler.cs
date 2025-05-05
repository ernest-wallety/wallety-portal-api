using MediatR;
using Wallety.Portal.Application.Commands.General;
using Wallety.Portal.Application.Dto.Customer;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Core.Enum;
using Wallety.Portal.Core.Helpers;
using Wallety.Portal.Core.Helpers.Constants;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Requests.Customer;

namespace Wallety.Portal.Application.Handlers.Customer
{
    public class VerifyCustomerAccountHandler(ICustomerRepository repository) :
        IRequestHandler<CreateCommand<CustomerVerificationDTO, CreateResponse>, CreateResponse>
    {
        private readonly ICustomerRepository _repository = repository;

        public async Task<CreateResponse> Handle(CreateCommand<CustomerVerificationDTO, CreateResponse> request, CancellationToken cancellationToken)
        {
            // var customerAccount = await _repository.GetCustomerById(request.Item.CustomerId!);

            var dto = new CustomerVerificationModel
            {
                CustomerId = EncryptionHelper.Decrypt(request.Item.CustomerId!),
                RegistrationStatusId = EncryptionHelper.Decrypt(request.Item.RegistrationStatusId!),
                VerificationRejectReasonId = EncryptionHelper.Decrypt(request.Item.VerificationRejectReasonId!)
            };


            if (Guid.Parse(dto.RegistrationStatusId) == RegistrationStatusConstants.REJECTED)
            {
                // var identifier = (customerAccount.Id != null) ? "SA ID." : "passport.";

                // await _watiService.SendConfirmationMessageAsync(customerAccount.PhoneNumber ?? "", $"{verificationRejectReason?.RejectMessage}");
                // await Task.Delay(TimeSpan.FromSeconds(3));
                // await _watiService.SendEditAccountDetailsTemplateAsync(customerAccount);
            }
            else
            {

                // await _watiService.SendSignUpMessageAsync(customerAccount);
            }

            var response = await _repository.VerifyCustomerAccount(dto);

            if (!response.IsSuccess)
                throw new ArgumentException(response.ResponseMessage).WithDisplayData(EnumValidationDisplay.Toastr); ;

            return LazyMapper.Mapper.Map<CreateResponse>(response);
        }
    }
}
