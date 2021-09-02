using Microsoft.Extensions.DependencyInjection;

namespace MongoDriver.Facade
{
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
        public static void AddMongoDbContext(this IServiceCollection services)
        {
            services.AddSingleton<MongoDbContext>();
        }

        /// <summary>
        /// Register the MongoDbContext.
        /// </summary>
        /// <typeparam name="TMongoDbContext">Represent the class that inherited from MongoDbContext.</typeparam>
        /// <param name="services">
        /// Microsoft.Extensions.DependencyInjection
        /// Specifies the contract for a collection of service descriptors.
        /// </param>
        public static void AddMongoDbContext<TMongoDbContext>(this IServiceCollection services) where TMongoDbContext : MongoDbContext
        {
            services.AddSingleton<MongoDbContext>();
            services.AddSingleton<TMongoDbContext>();
        }
    }
}