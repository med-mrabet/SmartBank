using MediatR;
using SmartBank.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Features.User.Transactions.AddTransaction
{
    public record AddTransactionCommand(AddTransactionDto transaction,Guid userId) : IRequest<AddTransactionDto>
    {
    }
}
