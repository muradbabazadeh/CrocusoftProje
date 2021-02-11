using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocusoftProje.Api.Infrastructure.ErrorHandling
{
    public class ValidationError
    {
        public ValidationError(string property, string errorMessage)
        {
            Property = property;
            ErrorMessage = errorMessage;
        }

        public string Property { get; }

        public string ErrorMessage { get; }
    }
}
