using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        /*** FluentValidation files register ***/
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        /*** Mediatr config ***/
        services.AddMediatR(cnf => 
            cnf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}
