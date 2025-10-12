using Mapster;
using MediatR;
using SmartBank.Application.Persistence;
using SmartBank.Shared.Dtos;


namespace SmartBank.Application.Features.User.Account.Commands.FreezAccount
{
    public class FreezAccountCommandHandler : IRequestHandler<FreezAccountCommand,Boolean>
    {
        private readonly IAccountRepository _accountRepository;
        public FreezAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Boolean> Handle(FreezAccountCommand request, CancellationToken cancellationToken)
        {
            var acc = await _accountRepository.FindByCreterieAsync(a => a.Id == request.accountId && a.UserId == request.userId);
            var accountDto = acc.Adapt<AccountDto>();
            if(accountDto.Balance>0)
            {
                throw new Exception("Account balance must be zero to freeze the account");
            }
            if (accountDto == null)
            {
                throw new Exception("Account not found");
            }

            if(accountDto.Status == AccountStatusDto.FREEZED)
            {
                throw new Exception("Account is already frozen");
            }

            accountDto.Status = AccountStatusDto.PENDING;
            accountDto.ActionName = "FREEZ";
            var account = accountDto.Adapt<SmartBank.Domain.Entities.Account>();
            await _accountRepository.UpdateAsync(account);
            return true;

        }
    }
}
