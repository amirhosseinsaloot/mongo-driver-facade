using MongoDB.Driver;

namespace MongoDriver.Facade.TestApi.Models
{
    public class Book : MongoDbCollection
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }

    public class BookConfigration : ICollectionConfiguration<Book>
    {
        public CreateCollectionOptions Configure()
        {
            return new CreateCollectionOptions { NoPadding = true };
        }
    }
}