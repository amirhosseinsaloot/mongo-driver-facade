using MongoDB.Driver;

namespace MongoDriver.Facade;

/// <summary>
/// Represents extensions methods on MongoDbContext.
/// </summary>
public static class MongoDbContextExtensions
{
    /// <summary>
    /// Extension method which return collection.
    /// This can be used to query.
    /// </summary>
    /// <typeparam name="TCollection">Collection model.</typeparam>
    /// <param name="mongoDbContext"></param>
    /// <returns>Generic IMongoCollection of collection type.</returns>
    public static IMongoCollection<TCollection> Set<TCollection>(this MongoDbContext mongoDbContext) where TCollection : MongoDbCollection
    {
        return mongoDbContext.Database.GetCollection<TCollection>(typeof(TCollection).Name);
    }
}
