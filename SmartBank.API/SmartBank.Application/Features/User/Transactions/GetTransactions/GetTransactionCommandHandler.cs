using Mapster;
using MediatR;
using SmartBank.Application.Persistence;
using SmartBank.Shared.Dtos;

namespace SmartBank.Application.Features.User.Transactions.GetTransactions
{
    internal class GetTransactionCommandHandler : IRequestHandler<GetTransactionCommand, List<GetTransactionDto>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public GetTransactionCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<List<GetTransactionDto>> Handle(GetTransactionCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _accountRepository.ExistByCreterieAsync(p => p.Id == request.accountId);
            if (!isExist)
            {
                throw new Exception("Account not found");
            }

            var transactions = await _transactionRepository.GetUserTransactionsByAccountIdAsync(request.accountId);
            var transactionDto = transactions.Adapt<List<GetTransactionDto>>();
            return transactionDto;
        }
    }
}
