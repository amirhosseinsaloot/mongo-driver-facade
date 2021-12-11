using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDriver.Facade;

public class MongoDbCollection
{
    /// <summary>
    /// Represent _id field of collection.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public virtual string Id { get; set; }
}
