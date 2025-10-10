
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartBank.Application.Identity;
using SmartBank.Identity.Service;
using SmartBank.Shared.Models;
using System.Text;

namespace SmartBank.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {

          
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();

          

            return services;

        }

    }
}
