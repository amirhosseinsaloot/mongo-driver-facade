namespace MongoDriver.Facade.TestApi.Models
{
    public class Location : MongoDbCollection
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Stree { get; set; }
    }
}