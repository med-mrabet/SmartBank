using MediatR;
using SmartBank.Shared.Dtos;


namespace SmartBank.Application.Features.User.Account.Commands.AddAccount
{
    public record AddAccountCommand(AccountDto account,Guid userId) : IRequest<bool>
    {
    }
}
