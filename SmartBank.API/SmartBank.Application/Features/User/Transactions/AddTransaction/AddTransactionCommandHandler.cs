using Mapster;
using MediatR;
using SmartBank.Application.Identity;
using SmartBank.Application.Persistence;
using SmartBank.Domain.Entities;
using SmartBank.Shared.Dtos;

namespace SmartBank.Application.Features.User.Transactions.AddTransaction
{
    public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, AddTransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserService _userService;

        public AddTransactionCommandHandler(ITransactionRepository transactionRepository, IUserService userService, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _userService = userService;
            _accountRepository = accountRepository;
        }

        public async Task<AddTransactionDto> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.ExistByIdAsync(request.userId);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            var fromAccount = await _accountRepository.GetByIdAsync(request.transaction.FromAccountId);
            var toAccount = await _accountRepository.GetByIdAsync(request.transaction.ToAccountId);
            if (fromAccount.Balance < request.transaction.Amount)
            {
                throw new Exception("Insufficient balance");
            }
            var isInternalTransaction = await _accountRepository.ExistByCreterieAsync(p=>p.Id == request.transaction.ToAccountId && p.UserId == request.userId);
            var trans = request.transaction.Adapt<Transaction>();
            trans.TransactionDate = DateTime.UtcNow;
            if (isInternalTransaction)
            {
                
                trans.Status = Domain.Enums.TransactionStatus.COMPLETED;
                 await _transactionRepository.AddAsync(trans);
                fromAccount.Balance -= request.transaction.Amount;
                await _accountRepository.UpdateAsync(fromAccount);
                toAccount.Balance += request.transaction.Amount;
                await _accountRepository.UpdateAsync(toAccount);
                return (trans.Adapt<AddTransactionDto>());
            }
            // External transaction 
            if(request.transaction.Amount > 2000)
            {
              
                trans.Status = Domain.Enums.TransactionStatus.PENDING;
                await _transactionRepository.AddAsync(trans);

            }
            await _transactionRepository.AddAsync(trans);
            fromAccount.Balance -= request.transaction.Amount;
            await _accountRepository.UpdateAsync(fromAccount);
            toAccount.Balance += request.transaction.Amount;
            await _accountRepository.UpdateAsync(toAccount);
            return (trans.Adapt<AddTransactionDto>());



        }
    }
}
