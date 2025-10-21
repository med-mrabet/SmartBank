using FluentValidation;
using MediatR;
using SmartBank.Application.Persistence;
using SmartBank.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Features.User.Account.Commands.UpdateAccount
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountDto>
    {
        private readonly IValidator<UpdateAccountCommand> _validator;
        private readonly IAuditLogRepository _auditRepository;

        public UpdateAccountCommandHandler(IValidator<UpdateAccountCommand> validator, IAuditLogRepository auditRepository)
        {
            _validator = validator;
            _auditRepository = auditRepository;
        }
        public Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
