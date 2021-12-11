namespace MongoDriver.Facade.TestApi.Models;

public class Person : MongoDbCollection
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Job { get; set; }

    public string Fathername { get; set; }
}
