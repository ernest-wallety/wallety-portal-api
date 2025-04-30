namespace Wallety.Portal.Core.Services
{
    public interface IPgSqlSelector
    {
        Task<List<T>> SelectQuery<T>(string query, object? parameters = null) where T : new();

        Task<T?> SelectFirstOrDefaultQuery<T>(string query, object? parameters = null) where T : new();

        Task<bool> ExecuteAsyncQuery(string query, object? parameters = null);

        Task<T?> ExecuteScalarAsyncQuery<T>(string query, object? parameters = null);

        Task<T?> ExecuteStoredProcedureAsync<T>(string query, object? parameters = null);
    }
}
