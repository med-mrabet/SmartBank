using Mapster;
using MediatR;
using SmartBank.Application.Persistence;
using SmartBank.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Features.User.Account.Queries.GetAccount
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, GetAccountDto>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<GetAccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.accountId);
            if (account == null)
            {
                throw new Exception("User does not have account");
            }
            var accountDto = account.Adapt<GetAccountDto>();
            return accountDto;
        }
    }
}
