using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocusoftProje.Api.Infrastructure.ErrorHandling
{
    public class JsonErrorResponse
    {
        public JsonErrorResponse(string errorType, string message, object developerMessage)
        {
            ErrorType = errorType;
            Message = message;
            DeveloperMessage = developerMessage;
        }

        public string ErrorType { get; }

        public string Message { get; }

        public object DeveloperMessage { get; }
    }
}
