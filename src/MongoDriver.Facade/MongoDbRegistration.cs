using Microsoft.Extensions.DependencyInjection;

namespace MongoDriver.Facade;

/// <summary>
/// Represents Extension methods on IServiceCollection.
/// </summary>
public static class MongoDbRegistration
{
    /// <summary>
    /// Register the MongoDbContext.
    /// </summary>
    /// <param name="services">
    /// Microsoft.Extensions.DependencyInjection
    /// Specifies the contract for a collection of service descriptors.
    /// </param>
    /// <param name="contextLifetime">The lifetime with which to register the MongoDbContext service in the container.</param>
    public static void AddMongoDbContext(this IServiceCollection services, ServiceLifetime contextLifetime = ServiceLifetime.Singleton)
    {
        switch (contextLifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton<MongoDbContext>();
                break;

            case ServiceLifetime.Scoped:
                services.AddScoped<MongoDbContext>();
                break;

            case ServiceLifetime.Transient:
                services.AddTransient<MongoDbContext>();
                break;

            default:
                services.AddSingleton<MongoDbContext>();
                break;
        }
    }

    /// <summary>
    /// Register the MongoDbContext.
    /// </summary>
    /// <param name="services">
    /// Microsoft.Extensions.DependencyInjection
    /// Specifies the contract for a collection of service descriptors.
    /// </param>
    /// <param name="contextLifetime">The lifetime with which to register the MongoDbContext service in the container.</param>
    public static void AddMongoDbContext<TMongoDbContext>(this IServiceCollection services, ServiceLifetime contextLifetime = ServiceLifetime.Singleton) 
        where TMongoDbContext : MongoDbContext
    {
        switch (contextLifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton<MongoDbContext>();
                services.AddSingleton<TMongoDbContext>();
                break;

            case ServiceLifetime.Scoped:
                services.AddScoped<MongoDbContext>();
                services.AddScoped<TMongoDbContext>();
                break;

            case ServiceLifetime.Transient:
                services.AddTransient<MongoDbContext>();
                services.AddTransient<TMongoDbContext>();
                break;

            default:
                services.AddSingleton<MongoDbContext>();
                services.AddSingleton<TMongoDbContext>();
                break;
        }
    }
}
