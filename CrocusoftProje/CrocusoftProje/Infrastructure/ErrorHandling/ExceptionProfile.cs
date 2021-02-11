using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CrocusoftProje.Api.Infrastructure.ErrorHandling
{
    public class ExceptionProfile : AutoMapper.Profile
    {
        public ExceptionProfile()
        {
            CreateMap<ValidationException, DeveloperException>();

            CreateMap<Exception, DeveloperException>();
        }
    }
}
