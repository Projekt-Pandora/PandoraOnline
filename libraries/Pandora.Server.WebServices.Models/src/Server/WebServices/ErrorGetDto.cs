using System.Collections.Generic;

namespace Pandora.Server.WebServices.Models
{
    public sealed class ErrorGetDto
    {
        public int StatusCode { get; set; }

        public int ErrorCode { get; set; }

        public string Path { get; set; }

#nullable enable
        public Dictionary<string, object?>? RouteValues { get; set; }
#nullable disable

        public string Message { get; set; }

        public string StackTrace { get; set; }
    }
}
