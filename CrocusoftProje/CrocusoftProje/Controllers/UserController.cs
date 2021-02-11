using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrocusoftProje.Api.Extensions;
using CrocusoftProje.Identity.Queries;
using CrocusoftProje.UserDetails.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrocusoftProje.Api.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserQueries _userQueries;

        public UserController(IMediator mediator, IUserQueries userQueries)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
        }

        [AllowAnonymous]  //FOR A WHILE
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            await _mediator.ExecuteIdentifiedCommand<RegisterUserCommand, bool>(command, requestId);

            return NoContent();
        }
    }

}
