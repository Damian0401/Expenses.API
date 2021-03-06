using System.Net;

namespace Application.Services.Utilities
{
    public class ServiceResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }

        public ServiceResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ServiceResponse(HttpStatusCode statusCode, string? message) 
        {
            StatusCode = statusCode;
            Message = message;
        }
    }

    public class ServiceResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public T? ResponseContent { get; set; }

        public ServiceResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ServiceResponse(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ServiceResponse(HttpStatusCode statusCode, T responseContent)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }

        public ServiceResponse(HttpStatusCode statusCode, string message, T responseContent)
        {
            StatusCode = statusCode;
            Message = message;
            ResponseContent = responseContent;
        }
    }
}