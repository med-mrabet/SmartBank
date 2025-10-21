using Mapster;
using MediatR;
using SmartBank.Application.Persistence;
using SmartBank.Domain.Enums;
using SmartBank.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Features.Admin.Accounts.FreezAccount
{
    public class FreezAccountByAdminCommandHandler : IRequestHandler<FreezAccountByAdminCommand, AccountDto>
    {
        public readonly IAccountRepository _accountRepository;
        public FreezAccountByAdminCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDto> Handle(FreezAccountByAdminCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.accountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            if(account.Status == AccountStatus.FREEZED)
            {
                throw new Exception("Account is already frozen");
            }
            if ((account.ActionName) == "FREEZ")
            {
                account.Status = AccountStatus.FREEZED;
                account.ActionName = null;
                await _accountRepository.UpdateAsync(account);
            }

            return account.Adapt<AccountDto>();

        }
    }
}
