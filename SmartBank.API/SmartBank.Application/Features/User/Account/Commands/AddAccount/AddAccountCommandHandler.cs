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
        private readonly IUserService _userService;
        public AddAccountCommandHandler(IValidator<AddAccountCommand> validator, IAccountRepository accountRepository, IUserService userService)
        {
            _validator = validator;
            _accountRepository = accountRepository;
            _userService = userService;
        }
        public async Task<bool> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var res = _validator.Validate(request);
            if (!res.IsValid) 
            {
                throw new Exception("Invalid input");
            }

            var user = await _userService.ExistByIdAsync(request.userId);
            if(user == null) 
            {
                throw new Exception("User not found");
            }

            
            var account = request.account.Adapt<SmartBank.Domain.Entities.Account>();
            account.UserId=request.userId;
            await _accountRepository.AddAsync(account);
            return true;

  
        }
    }
}
