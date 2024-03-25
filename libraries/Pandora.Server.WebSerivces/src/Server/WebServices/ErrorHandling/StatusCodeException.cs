using System;

namespace Pandora.Server.WebServices
{
    public class StatusCodeException : Exception
    {
        public StatusCodeException(int statusCode, int errorCode)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public StatusCodeException(int statusCodes, int errorCode, string message) : base(message)
        {
            StatusCode = statusCodes;
            ErrorCode = errorCode;
        }

        public StatusCodeException(int statusCodes, int errorCode, string message, Exception inner) : base(message, inner)
        {
            StatusCode = statusCodes;
            ErrorCode = errorCode;
        }

        public int StatusCode { get; }

        public int ErrorCode { get;  }
    }
}
