namespace MongoDriver.Facade.TestApi.Models;

public class Food : MongoDbCollection
{
    public string Foodname { get; set; }

    public string Price { get; set; }
}
