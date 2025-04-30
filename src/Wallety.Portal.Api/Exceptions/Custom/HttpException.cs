namespace Wallety.Portal.Api.Exceptions.Custom
{
    public class HttpException(string message, int statusCode) : Exception(message)
    {
        public int StatusCode { get; } = statusCode;
    }
}
