using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDriver.Facade;

public class MongoDbContext
{
    #region Properties

    /// <summary>
    /// Base class for implementors of MongoDB.Driver.IMongoClient.
    /// </summary>
    public MongoClient Client { get; }

    /// <summary>
    /// Represents a database in MongoDB.
    /// </summary>
    public IMongoDatabase Database { get; }

    /// <summary>
    /// List of collections in database. 
    /// </summary>
    public List<string> Collections { get; }

    /// <summary>
    /// Represent key value pairs of collection name and its configuration.
    /// </summary>
    public Dictionary<string, CreateCollectionOptions> CollectionConfigurations { get; } = new Dictionary<string, CreateCollectionOptions>();

    #endregion

    #region Ctor

    /// <summary>
    /// Initializes a new instance of the MongoDBContext class.
    /// </summary>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <exception>Exception occured when MongoSettings (in appsettings.json) has not correct format in appsettings.json.</exception>
    public MongoDbContext(IConfiguration configuration)
    {
        if (configuration.GetSection("MongoSettings:ConnectionString").Value is null || configuration.GetSection("MongoSettings:Database").Value is null)
        {
            throw new Exception("Put MongoSettings in appsettings.json correctly.");
        }

        Client = new MongoClient(configuration.GetSection("MongoSettings:ConnectionString").Value);
        Database = GetOrCreateDatabase(configuration.GetSection("MongoSettings:Database").Value);
        Collections = GetDbCollections();
        CollectionConfigurations = new ReflectionHelper().GetCollections();
        CreateDefinedCollectionsIfNotExists();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Ensures that the database for the context exists. If it exists, get the database,
    /// If it does not exist then the new database is created.
    /// </summary>
    /// /// <param name="databaseName">Database name to be removed.</param>
    /// <returns>Database (IMongoDatabase).</returns>
    private IMongoDatabase GetOrCreateDatabase(string databaseName)
    {
        return Client.GetDatabase(databaseName);
    }

    /// <summary>
    /// Get the whole collections of database.
    /// </summary>
    /// <returns>String list included collections name.</returns>
    private List<string> GetDbCollections()
    {
        return Database.ListCollectionNames().ToList();
    }

    /// <summary>
    /// Create all defined collection if they doesn't exists in mongo database.
    /// </summary>
    private void CreateDefinedCollectionsIfNotExists()
    {
        foreach (var item in CollectionConfigurations)
        {
            if (Collections.Any(p => p == item.Key) is false)
            {
                try
                {
                    Database.CreateCollection(item.Key, item.Value);
                    Collections.Add(item.Key);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error occured during creation of {item.Key} collection.See the inner exception.", ex);
                }
            }
        }
    }

    #endregion
}
