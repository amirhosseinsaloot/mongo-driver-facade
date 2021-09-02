namespace MongoDriver.Facade.TestApi.Models
{
    public class Post : MongoDbCollection
    {
        public string Content { get; set; }

        public string Title { get; set; }

        public Author Author { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public Post Posts { get; set; }
    }
}