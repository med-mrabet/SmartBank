using Mapster;
using MediatR;
using SmartBank.Application.Exceptions;
using SmartBank.Application.Persistence;
using SmartBank.Domain.Entities;
using SmartBank.Domain.Enums;
using SmartBank.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Features.Admin.Transactions.RejectTransaction
{
    public class RejectTransactionCommandHandler : IRequestHandler<RejectTransactionCommand, GetTransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuditLogRepository _auditRepository;


        public RejectTransactionCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IAuditLogRepository auditRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _auditRepository = auditRepository;
        }

        public async Task<GetTransactionDto> Handle(RejectTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(request.Id);
            if (transaction == null)
            {
                throw new NotFoundException("Transaction",transaction.Id);
            }
            if (transaction.Status != TransactionStatus.PENDING)
            {
                throw new BadRequestException("Transaction is not pending");
            }
            transaction.Status = TransactionStatus.REJECTED;
            await _transactionRepository.UpdateAsync(transaction);
            await _auditRepository.AddAsync(new Domain.Entities.AuditLog
            {
                Action = "UPDATE",
                EntityType = nameof(SmartBank.Domain.Entities.Transaction),
                IsRead = true,
                OldValue = $"Transaction {transaction.Id} status: {TransactionStatus.PENDING}",
                NewValue = $"Transaction {transaction.Id} status: {TransactionStatus.REJECTED}"
            });
            return transaction.Adapt<GetTransactionDto>();
        }
    }
}
