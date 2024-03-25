using System;
using Microsoft.AspNetCore.Http;

namespace Pandora.Server.WebServices
{
    public class UnprocessableEntityException : StatusCodeException
    {
        public UnprocessableEntityException(int errorCode) : base(StatusCodes.Status422UnprocessableEntity, errorCode)
        { }

        public UnprocessableEntityException(int errorCode, string message) : base(StatusCodes.Status422UnprocessableEntity, errorCode, message)
        { }

        public UnprocessableEntityException(int errorCode, string message, Exception inner) : base(StatusCodes.Status422UnprocessableEntity, errorCode, message, inner)
        { }
    }
}