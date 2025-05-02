using System.Text.Json;
using MediatR;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Core.Helpers.Constants;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Handlers.Transactions
{
    public class GetTransactionsHandler(ITransactionHistoryRepository repository) :
        IRequestHandler<ListQuery<TransactionHistoryResponse>, Pagination<TransactionHistoryResponse>>
    {
        private readonly ITransactionHistoryRepository _repository = repository;

        public async Task<Pagination<TransactionHistoryResponse>> Handle(ListQuery<TransactionHistoryResponse> request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetTransactions(request.Criteria);

            var response = LazyMapper.Mapper.Map<Pagination<TransactionHistoryResponse>>(items);

            foreach (var item in response.Items)
            {
                var result = await _repository.GetTransactionByReferenceQuery(item.TransactionReference);

                var historyJson = JsonSerializer.Deserialize<List<TransactionHistoryResponse>>(result.TransactionHistoryJson);

                historyJson = [.. historyJson!.OrderByDescending(t => t.TransactionDate)];

                item.TransactionStatus = historyJson!.FirstOrDefault()?.TransactionStatus!;

                // Assuming that refund transactions are always the first in the list
                // var firstHistory = historyJson.FirstOrDefault();

                // if (firstHistory != null && firstHistory.TransactionTypeName.Contains("Refund"))
                // {
                //     item.TransactionStatus = historyJson.Where(x => x.IsComplete && x.TransactionTypeName != "Refund").FirstOrDefault()?.TransactionStatus!;
                // }

                foreach (var transaction in historyJson)
                {
                    if (transaction.TransactionTypeId == TransactionTypeConstants.WALLETY_REFUND) item.TransactionStatus = historyJson.Where(x => x.IsComplete && x.TransactionTypeId != TransactionTypeConstants.WALLETY_REFUND).FirstOrDefault()?.TransactionStatus!;
                }
            }

            return response;
        }
    }
}
