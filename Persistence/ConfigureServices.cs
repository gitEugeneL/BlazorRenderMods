using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services
            .AddScoped<IAppDbContext, AppDbContext>();
        
        services.AddDbContext<AppDbContext>(option => 
            option.UseSqlServer(configuration.GetConnectionString("SQLServer")!));
        
        return services;
    }
}
