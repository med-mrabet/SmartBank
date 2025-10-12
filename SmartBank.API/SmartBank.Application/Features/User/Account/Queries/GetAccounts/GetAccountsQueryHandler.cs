using Mapster;
using MediatR;
using SmartBank.Application.Persistence;
using SmartBank.Shared.Dtos;


namespace SmartBank.Application.Features.User.Account.Queries.GetAccounts
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<GetAccountDto>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<List<GetAccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.FindByCreterieAsync(p=>p.UserId == request.UserId);
            var accountsDtos = accounts.Adapt<List<GetAccountDto>>();
            return accountsDtos;
        }
    }
}
