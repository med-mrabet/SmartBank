using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using SmartBank.Infrastructure.Context;

namespace SmartBank.Infrastructure;

public static class SmartBankInfrastructure
{
    public static IServiceCollection AddInfrastructureServices( this IServiceCollection services, IConfiguration configuration)
    {
        // Extension method to add infrastructure services to the IServiceCollection
         string dbConnString = configuration.GetConnectionString("SmartBankDatabase")!;

        services.AddDbContext<BankSmartContext>(opt =>
        opt.UseSqlServer(dbConnString));

        return services;
    }
}
