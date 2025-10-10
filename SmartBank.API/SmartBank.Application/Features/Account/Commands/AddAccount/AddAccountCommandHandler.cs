using FluentValidation;
using MediatR;
using SmartBank.Application.Identity;


namespace SmartBank.Application.Features.Account.Commands.AddAccount
{
    public class AddAccountCommandHandler : IRequestHandler<AddAccountCommand, bool>
    {
        private readonly IValidator<AddAccountCommand> _validator;
        public AddAccountCommandHandler(IValidator<AddAccountCommand> validator)
        {
            _validator = validator;
        }
        public async Task<bool> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var res = _validator.Validate(request);
            throw new NotImplementedException();
  
        }
    }
}
