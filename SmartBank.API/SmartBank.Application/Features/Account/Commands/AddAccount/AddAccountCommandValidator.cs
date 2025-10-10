using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Features.Account.Commands.AddAccount
{
    public class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
    {
        public AddAccountCommandValidator() 
        {
            RuleFor(p => p.account.AccountType)
            .NotNull().WithMessage("Account type must be not null ");
            RuleFor(p => p.account.Balance)
             .GreaterThan(0)
            .WithMessage("Balance must be greater than 0 ")
            .NotNull().WithMessage("Balance must be not null ");

            RuleFor(p => p.account.Currency)
          .NotNull().WithMessage("Balance must be not null ")
          .MaximumLength(10);



        }
    }
}
