using MediatR;
using SmartBank.Shared.Dtos;


namespace SmartBank.Application.Features.Account.Commands.AddAccount
{
    public record AddAccountCommand(AccountDto account) : IRequest<bool>
    {
    }
}
