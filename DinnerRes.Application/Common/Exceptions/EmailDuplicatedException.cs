using System.Net;

namespace DinnerRes.Application.Common.Exceptions;

public sealed class EmailDuplicatedException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Something went wrong.";
}