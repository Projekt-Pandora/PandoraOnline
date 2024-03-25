using System;
using Microsoft.AspNetCore.Http;

namespace Pandora.Server.WebServices
{
    public class BadRequestException : StatusCodeException
    {
        public BadRequestException(int errorCode) : base(StatusCodes.Status400BadRequest, errorCode)
        { }

        public BadRequestException(int errorCode, string message) : base(StatusCodes.Status400BadRequest, errorCode, message)
        { }

        public BadRequestException(int errorCode, string message, Exception inner) : base(StatusCodes.Status400BadRequest, errorCode, message, inner)
        { }
    }
}