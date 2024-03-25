using System;
using Microsoft.AspNetCore.Http;

namespace Pandora.Server.WebServices
{
    public class NotFoundException : StatusCodeException
    {
        public NotFoundException(int errorCode) : base(StatusCodes.Status404NotFound, errorCode)
        { }

        public NotFoundException(int errorCode, string message) : base(StatusCodes.Status404NotFound, errorCode, message)
        { }

        public NotFoundException(int errorCode, string message, Exception inner) : base(StatusCodes.Status404NotFound, errorCode, message, inner)
        { }
    }
}