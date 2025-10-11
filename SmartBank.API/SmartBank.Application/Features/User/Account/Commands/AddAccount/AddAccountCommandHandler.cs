using FluentValidation;
using Mapster;
using MediatR;
using SmartBank.Application.Identity;
using SmartBank.Application.Persistence;
using SmartBank.Domain.Entities;


namespace SmartBank.Application.Features.User.Account.Commands.AddAccount
{
    public class AddAccountCommandHandler : IRequestHandler<AddAccountCommand, bool>
    {
        private readonly IValidator<AddAccountCommand> _validator;
        private readonly IAccountRepository _accountRepository;
        public AddAccountCommandHandler(IValidator<AddAccountCommand> validator, IAccountRepository accountRepository)
        {
            _validator = validator;
            _accountRepository = accountRepository;
        }
        public async Task<bool> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var res = _validator.Validate(request);
            if (!res.IsValid) 
            {
                throw new Exception("Invalid input");
            }

            var account = request.account.Adapt<SmartBank.Domain.Entities.Account>();
            await _accountRepository.AddAsync(account);
            return true;

  
        }
    }
}
