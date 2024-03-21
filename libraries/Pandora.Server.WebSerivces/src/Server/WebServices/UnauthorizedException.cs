using System;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace Pandora.Server.WebServices
{
    public class UnauthorizedException : StatusCodeException
    {
        public UnauthorizedException(int errorCode) : base(StatusCodes.Status401Unauthorized, errorCode)
        { }

        public UnauthorizedException(int errorCode, string message) : base(StatusCodes.Status401Unauthorized, errorCode, message)
        { }

        public UnauthorizedException(int errorCode, string message, Exception inner) : base(StatusCodes.Status401Unauthorized, errorCode, message, inner)
        { }
    }
}