using MongoDB.Driver;

namespace MongoDriver.Facade
{
    /// <summary>
    /// Allows configuration for an collection type to be factored into a separate class.
    /// Implement this interface, applying configuration for the collection type.
    /// </summary>
    /// <typeparam name="TCollection">The collection type to be configured.</typeparam>
    public interface ICollectionConfiguration<TCollection> where TCollection : MongoDbCollection
    {
        /// <summary>
        /// Configures the collection of type TCollection.
        /// </summary>
        /// <returns>
        /// CreateCollectionOptions of TCollection.
        /// If there is special configuration for 
        /// the collection, implement Configre method and
        /// return new instance of CreateCollectionOptions.
        /// </returns>
        public CreateCollectionOptions Configure();
    }
}