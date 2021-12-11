using MongoDB.Driver;

namespace MongoDriver.Facade.TestApi.Models;

public class Product : MongoDbCollection
{
    public string Name { get; set; }

    public string Category { get; set; }

    public string Price { get; set; }
}

public class ProductConfigration : ICollectionConfiguration<Product>
{
    public CreateCollectionOptions Configure()
    {
        return new CreateCollectionOptions { Capped = true, NoPadding = true, MaxSize = 25 };
    }
}
