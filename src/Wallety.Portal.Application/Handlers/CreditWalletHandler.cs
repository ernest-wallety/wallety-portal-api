// using MediatR;
// using Wallety.Portal.Application.Commands.General;
// using Wallety.Portal.Application.Dto;
// using Wallety.Portal.Application.Response.General;
// using Wallety.Portal.Core.Enum;
// using Wallety.Portal.Core.Repository;
// using Wallety.Portal.Core.Helpers;
// using Wallety.Portal.Core.Helpers.Constants;
// using Wallety.Portal.Core.Services;
// using System.Globalization;


// namespace Wallety.Portal.Application.Handlers
// {
//     public class CreditWalletHandler(
//         IUserRepository userRepository,
//         ITransactionRepository transactionRepository,
//         IConfigurationService config
//     ) : IRequestHandler<UpdateCommand<CreditWalletDTO, UpdateResponse>, UpdateResponse>
//     {
//         private readonly IUserRepository _userRepository = userRepository;
//         private readonly ITransactionRepository _transactionRepository = transactionRepository;

//         private readonly List<string> ALLOWED_CREDIT_WALLET_ACCOUNTS = config.AllowedCreditWalletAccounts().ToStringList();
//         private readonly int MAX_CREDIT_WALLET_ACCOUNT = config.CreditLimit();

//         private bool PHONE_CHECK { get; set; }
//         private bool EMAIL_CHECK { get; set; }

//         public async Task<UpdateResponse> Handle(UpdateCommand<CreditWalletDTO, UpdateResponse> request, CancellationToken cancellationToken)
//         {
//             var existingUser = await _userRepository.GetUserByEmail()
//                 ?? throw new ArgumentException("The provided email or username is invalid.").WithDisplayData(EnumValidationDisplay.Toastr);

//             var userSession = (await _userRepository.GetUserSession(existingUser.UserId)).Items.Where(x => x.IsActive).OrderByDescending(u => u.StartTime).FirstOrDefault()
//                 ?? throw new ArgumentException("No active session found.").WithDisplayData(EnumValidationDisplay.Toastr);

//             if (existingUser.RoleId != RoleConstants.EXECUTIVE.ToString())
//                 throw new ArgumentException("You are not authorized to perform this action.").WithDisplayData(EnumValidationDisplay.Toastr);

//             PHONE_CHECK = string.IsNullOrEmpty(existingUser.PhoneNumber) && !ALLOWED_CREDIT_WALLET_ACCOUNTS.Contains(existingUser.PhoneNumber!);
//             EMAIL_CHECK = string.IsNullOrEmpty(existingUser.Email) && !ALLOWED_CREDIT_WALLET_ACCOUNTS.Contains(existingUser.Email!);

//             if (PHONE_CHECK || EMAIL_CHECK)
//                 throw new ArgumentException("You are not authorized to perform this action.").WithDisplayData(EnumValidationDisplay.Toastr);

//             var creditAccount = await _userRepository.GetUserByCellNumber(request.Item!.WhatsappNumber);

//             if (creditAccount == null)
//                 throw new ArgumentException("Wallet account not found.").WithDisplayData(EnumValidationDisplay.Toastr);
//             else
//             {
//                 if (string.IsNullOrEmpty(creditAccount.AccountNumber))
//                     throw new ArgumentException("Beneficiary's account not creditable.").WithDisplayData(EnumValidationDisplay.Toastr);
//                 if (creditAccount.IsAccountActive)
//                     throw new ArgumentException("User's account is inactive.").WithDisplayData(EnumValidationDisplay.Toastr);
//                 if (creditAccount.IsFrozen)
//                     throw new ArgumentException("User's account is frozen.").WithDisplayData(EnumValidationDisplay.Toastr);
//                 if (creditAccount.IsVerified)
//                     throw new ArgumentException("User's account is unverified.").WithDisplayData(EnumValidationDisplay.Toastr);
//             }

//             if (request.Item.Amount > MAX_CREDIT_WALLET_ACCOUNT)
//                 throw new ArgumentException($"The current credit limit is {MAX_CREDIT_WALLET_ACCOUNT.ToString("C", CultureInfo.CreateSpecificCulture("en-ZA"))}").WithDisplayData(EnumValidationDisplay.Toastr);

//             //Create transaction
//             var createTransaction = await _transactionRepository.CreateCreditWalletTransaction(userSession, creditAccount, request.Item.Amount);

//             if (!createTransaction.IsSuccess)
//                 throw new ArgumentException(createTransaction.ResponseMessage).WithDisplayData(EnumValidationDisplay.Toastr);

//             // return LazyMapper.Mapper.Map<UpdateResponse>(result);
//             return null;
//         }

//     }
// }
