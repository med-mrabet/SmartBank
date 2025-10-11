using MediatR;


namespace SmartBank.Application.Features.User.Account.Commands.FreezAccount
{
    public class FreezAccountCommandHandler : IRequestHandler<FreezAccountCommand,Boolean>
    {
        public Task<Boolean> Handle(FreezAccountCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
