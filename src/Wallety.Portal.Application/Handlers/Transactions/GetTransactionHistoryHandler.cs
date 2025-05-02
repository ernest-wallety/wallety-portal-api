using System.Text.Json;
using MediatR;
using Wallety.Portal.Application.Mapper;
using Wallety.Portal.Application.Queries.General;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Core.Repository;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Handlers.Transactions
{
    public class GetTransactionHistoryHandler(ITransactionHistoryRepository repository)
    : IRequestHandler<ListAllQuery<TransactionHistoryResponse>, DataList<TransactionHistoryResponse>>
    {
        private readonly ITransactionHistoryRepository _repository = repository;

        public async Task<DataList<TransactionHistoryResponse>> Handle(ListAllQuery<TransactionHistoryResponse> request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetTransactionByReferenceQuery(request.STR!) ?? throw new ArgumentException("Transaction reference cannot be null or empty.", nameof(request.STR));

            var historyJson = JsonSerializer.Deserialize<List<TransactionHistoryResponse>>(items.TransactionHistoryJson);

            historyJson = [.. historyJson!.OrderByDescending(t => t.TransactionDate)];

            var results = new DataList<TransactionHistoryResponse>
            {
                Items = historyJson,
                Count = historyJson.Count
            };

            var response = LazyMapper.Mapper.Map<DataList<TransactionHistoryResponse>>(results);

            return response;
        }
    }
}
