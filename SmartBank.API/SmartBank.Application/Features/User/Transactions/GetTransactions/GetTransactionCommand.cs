using MediatR;
using SmartBank.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Features.User.Transactions.GetTransactions
{
    public record GetTransactionCommand(Guid accountId) :IRequest<List<GetTransactionDto>>
    {
    }
}
