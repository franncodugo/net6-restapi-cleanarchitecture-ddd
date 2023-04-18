using System.Net;

namespace DinnerRes.Application.Common.Exceptions;

public interface IServiceException
{
    public HttpStatusCode StatusCode { get;}
    public string ErrorMessage { get;}
}