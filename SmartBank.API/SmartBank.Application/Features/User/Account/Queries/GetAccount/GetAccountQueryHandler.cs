using MediatR;
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
        public Task<GetAccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
