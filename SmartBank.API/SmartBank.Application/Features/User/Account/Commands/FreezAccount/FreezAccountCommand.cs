using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Features.User.Account.Commands.FreezAccount
{
    public record FreezAccountCommand(Guid userId , Guid accountId):IRequest<Boolean>
    {
    }
}
