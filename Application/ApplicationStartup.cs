using Application.Services;
using Domain.Entities;
using Domain.Entities.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application;

public static class ApplicationStartup
{
    public static IServiceCollection TryAddApplication(this IServiceCollection services)
    {
        services.TryAddScoped<BaseService<Trader>>();
        services.TryAddScoped<BaseService<Item>>();
        services.TryAddScoped<BaseService<Category>>();
        services.TryAddScoped<BaseService<Discount>>();
        services.TryAddScoped<BaseService<Player>>();
        services.TryAddScoped<BaseService<TraderItem>>();
        
        services.TryAddSingleton<CurrentStateService>();
        
        return services;
    }
}