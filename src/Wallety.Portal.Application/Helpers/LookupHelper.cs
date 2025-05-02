using Wallety.Portal.Core.Helpers;
using Wallety.Portal.Core.Helpers.Constants;
using Wallety.Portal.Core.Requests.Common;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Helpers
{
    public static class LookupHelper
    {
        public static DataList<LookupModel> RegistrationStatuses(DataList<LookupModel> items, bool isFilter)
        {
            var result = items.Items
                .Where(u =>
                    (isFilter && u.Id?.ToString() != RegistrationStatusConstants.APPROVED.ToString()) ||
                    (!isFilter && u.Id?.ToString() != RegistrationStatusConstants.RESUBMITTED.ToString() && u.Id?.ToString() != RegistrationStatusConstants.PENDING.ToString()))
                .Select(u =>
                {
                    u.PrimaryKey = u.Id?.ToString()!;
                    return u;
                })
                .ToList();

            return new DataList<LookupModel>
            {
                Items = result,
                Count = result.Count
            };
        }

        public static DataList<LookupModel> VerificationRejectReasons(DataList<LookupModel> items)
        {
            var result = items.Items = [.. items.Items
                    .Select(u =>
                    {
                        u.PrimaryKey = u.Id?.ToString()!;
                        u.Id = EncryptionHelper.Encrypt(u.Id?.ToString()!);
                        return u;
                    })];

            return new DataList<LookupModel>
            {
                Items = result,
                Count = result.Count
            };
        }

        public static DataList<LookupModel> Roles(DataList<LookupModel> items, bool isFilter)
        {
            var result = items.Items = [.. items.Items.Where(u => !isFilter || (u.Id?.ToString() != RoleConstants.CUSTOMER.ToString() && u.Id?.ToString() != RoleConstants.EXECUTIVE.ToString()))];

            return new DataList<LookupModel>
            {
                Items = result,
                Count = result.Count
            };
        }
    }
}
