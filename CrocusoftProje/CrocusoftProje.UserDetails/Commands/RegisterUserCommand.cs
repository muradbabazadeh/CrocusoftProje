using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.UserDetails.Commands
{
   public class RegisterUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
    }
}
