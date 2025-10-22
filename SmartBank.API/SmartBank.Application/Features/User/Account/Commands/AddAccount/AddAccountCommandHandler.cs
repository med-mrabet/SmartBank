using FluentValidation;
using Mapster;
using MediatR;
using SmartBank.Application.Exceptions;
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
        private readonly IAuditLogRepository _auditRepository;
        public AddAccountCommandHandler(IValidator<AddAccountCommand> validator, IAccountRepository accountRepository, IUserService userService, IAuditLogRepository auditRepository)
        {
            _validator = validator;
            _accountRepository = accountRepository;
            _userService = userService;
            _auditRepository = auditRepository;
        }
        public async Task<bool> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var res = _validator.Validate(request);
            if (!res.IsValid) 
            {
                throw new BadRequestException("Invalid input");
            }

            var user = await _userService.ExistByIdAsync(request.userId);
            if(user == null) 
            {
                throw new NotFoundException("User",user.Id);
            }

            
            var account = request.account.Adapt<SmartBank.Domain.Entities.Account>();
            account.UserId=request.userId;
            await _accountRepository.AddAsync(account);
            await _auditRepository.AddAsync(new AuditLog
            {
                Action = "Account Creation",
                EntityType = nameof(Account),
                IsRead = true,
                UserId = request.userId,
                OldValue = "",
                NewValue = $"Account {account.Id} created for user {user.FirstName} {user.LastName}"
            });
            return true;

  
        }
    }
}
