using CrocusoftProje.Domain.AggregatesModel.UserAggregate;
using CrocusoftProje.Domain.Exceptions;
using CrocusoftProje.Identity.Queries;
using CrocusoftProje.Infrastructure.Commands;
using CrocusoftProje.Infrastructure.Idempotency;
using CrocusoftProje.SharedKernel.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrocusoftProje.UserDetails.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserQueries _userQueries;

        public RegisterUserCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            _userRepository = userRepository;
            _userQueries = userQueries;
        }

        public async Task<bool> Handle(RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var userName = request.UserName.ToLower();
            var existingUser = await _userQueries.FindByNameAsync(userName);

            if (existingUser != null)
            {
                throw new DomainException($"User name '{request.UserName}' already taken, please choose another name.");
            }

            var user = new Domain.AggregatesModel.UserAggregate.User(userName, request.Email, null, PasswordHasher.HashPassword(userName, request.Password), true, false, request.FirstName, request.LastName, request.Birthday);

            await _userRepository.AddAsync(user);

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class RegisterUserIdentifiedCommandHandler :
         IdentifiedCommandHandler<RegisterUserCommand, bool>
    {
        public RegisterUserIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests
        }
    }
}
