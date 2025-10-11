using MediatR;
using SmartBank.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Features.User.Account.Queries.GetAccounts
{
    public record GetAccountsQuery(Guid UserId) : IRequest<List<GetAccountDto>>
    {
    }
}
