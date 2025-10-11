using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Features.User.Account.Commands.UpdateAccount
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(p => p.account.Balance)
             .GreaterThan(0)
            .WithMessage("Balance must be greater than 0 ");
            RuleFor(p => p.account.Currency)
         .NotNull().WithMessage("Balance must be not null ")
         .MaximumLength(10);
        }
    }
}
