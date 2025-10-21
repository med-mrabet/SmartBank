using Mapster;
using MediatR;
using SmartBank.Application.Persistence;
using SmartBank.Domain.Entities;
using SmartBank.Shared.Dtos;

namespace SmartBank.Application.Features.Admin.Transactions.ApproveTransaction
{
    public class ApproveTransactionCommandHandler : IRequestHandler<ApproveTransactionCommand, GetTransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuditLogRepository _auditRepository;

        public ApproveTransactionCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IAuditLogRepository auditRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _auditRepository = auditRepository;
        }

        public async Task<GetTransactionDto> Handle(ApproveTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction =  await _transactionRepository.GetByIdAsync(request.transactionId);
            if(transaction == null)
            {
                throw new Exception("Transaction not found");
            }
            if(transaction.Status != Domain.Enums.TransactionStatus.PENDING)
            {
                throw new Exception("Transaction is not pending");
            }
            transaction.Status = Domain.Enums.TransactionStatus.COMPLETED;
            await _transactionRepository.UpdateAsync(transaction);
            var fromAccount = await _accountRepository.GetByIdAsync(transaction.FromAccountId);
            await _auditRepository.AddAsync(new AuditLog
            {
                Action = "UPDATE",
                EntityType = nameof(Transaction),
                IsRead = true,
                
                OldValue = $"Transaction {transaction.Id} status: {TransactionStatusDto.PENDING}",
                NewValue = $"Transaction {transaction.Id} status: {TransactionStatusDto.COMPLETED}"
            });
            var toAccount = await _accountRepository.GetByIdAsync(transaction.ToAccountId);
            var auditList = new List<AuditLog>
            {
                new AuditLog
                {
                     Action = "UPDATE",
                EntityType = nameof(Account),
                IsRead = true,

                OldValue = $"Account {fromAccount.Id} Balance: {fromAccount.Balance}",
                NewValue = $"Account {fromAccount.Id} Balance: {fromAccount.Balance - transaction.Amount}"
                },
                 new AuditLog
                {
                     Action = "UPDATE",
                EntityType = nameof(Account),
                IsRead = true,

                OldValue = $"Account {toAccount.Id} Balance: {toAccount.Balance}",
                NewValue = $"Account {toAccount.Id} Balance: {toAccount.Balance + transaction.Amount}"
                }
            };
            fromAccount.Balance -= transaction.Amount;
            toAccount.Balance += transaction.Amount;
            var accounts = new List<Account> { fromAccount, toAccount };
            await _accountRepository.BulkUpdateAsync(accounts);

            await _auditRepository.BulkAddAsync(auditList);
            return transaction.Adapt<GetTransactionDto>();
        }
    }
}
