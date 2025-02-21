using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure;

public static class InfrastructureStartup
{
    public static IServiceCollection TryAddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContextFactory<PostgreDbContext>();
        return services;
    }
    
    public static void CheckAndMigrateDatabase(IServiceScope scope)
    {
        Log.Information("Начинается проверка и миграция базы данных.");
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PostgreDbContext>();
            dbContext.Database.Migrate();
        }
        catch (Exception e)
        {
            Log.Error($"Ошибка во время миграции базы данных: {e}");
            throw;
        }
        Log.Information("Миграция базы данных успешно выполнена.");
    }
}