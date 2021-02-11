using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocusoftProje.Api.Infrastructure.ErrorHandling
{
    public class DeveloperException
    {
        public string Message { get; set; }

        public DeveloperException InnerException { get; set; }

        public IEnumerable<FluentValidation.Results.ValidationFailure> Errors { get; set; }
    }
}
