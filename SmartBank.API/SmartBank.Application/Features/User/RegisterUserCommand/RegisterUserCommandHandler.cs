using MediatR;
using SmartBank.Application.Identity;
using SmartBank.Application.Persistence;


namespace SmartBank.Application.Features.User.RegisterUserCommand
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand , Boolean>
    {
        private readonly IUserService _userService;
        public RegisterUserCommandHandler(IUserService userService)
        {
            this._userService = userService;
        }
        public async Task<Boolean> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
              var isRegistered = await _userService.RegisterCustomer(request.user);
               return isRegistered;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
