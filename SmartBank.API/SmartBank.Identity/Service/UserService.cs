using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SmartBank.Application.Identity;
using SmartBank.Shared.Dtos;
using SmartBank.Shared.Models;



namespace SmartBank.Identity.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApplicationUser?> ExistByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user;

        }

        public async Task<Boolean> RegisterCustomer(RegisterUser user)
        {
            try
            {
                var hasher = new PasswordHasher<ApplicationUser>();
                ApplicationUser userIdentity = new ApplicationUser
                {
                    UserName = user.FirstName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PasswordHash = hasher.HashPassword(null, user.Password),
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                };
                var result = await _userManager.CreateAsync(userIdentity, user.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userIdentity, "User");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
    }
}