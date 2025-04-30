namespace Wallety.Portal.Api.Exceptions.Custom
{
    public class RequestTimeoutException(string message) : HttpException(message, StatusCodes.Status408RequestTimeout) { }
}
