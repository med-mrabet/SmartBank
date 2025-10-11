using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using SmartBank.Application.Persistence;
using SmartBank.Infrastructure.Context;
using SmartBank.Infrastructure.Repositories;
using SmartBank.Shared.Dtos;
using SmartBank.Shared.Models;
using System.Text;

namespace SmartBank.Infrastructure;

public static class SmartBankInfrastructure
{
    public static IServiceCollection AddInfrastructureServices( this IServiceCollection services, IConfiguration configuration)
    {
        // Extension method to add infrastructure services to the IServiceCollection
         string dbConnString = configuration.GetConnectionString("SmartBankDatabase")!;

        services.AddDbContext<BankSmartContext>(opt =>
        opt.UseSqlServer(dbConnString));

        services.AddIdentity<ApplicationUser, ApplicationRole>().AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<BankSmartContext>().AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))

            };
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
        });

        services.AddScoped<IAccountRepository, AccountRepository>();
        return services;
    }
}
