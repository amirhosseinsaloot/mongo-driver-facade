<p align="center">
  <img width="200" height="200" src="https://user-images.githubusercontent.com/39926422/131835757-e2c10384-de5a-4d99-9f74-3dcac30f1182.png">
</p>


# mongo-driver-facade

[![.NET](https://github.com/amirhosseinsaloot/mongo-driver-facade/actions/workflows/dotnetcore.yml/badge.svg)](https://github.com/amirhosseinsaloot/mongo-driver-facade/actions/workflows/dotnetcore.yml)

What is the MongoDriver.Facade?
=====================
In short MongoDriver.Facade implements facade pattern on the [MongoDb C# driver](https://github.com/mongodb/mongo-csharp-driver).

Actually MongoDriver.Facade makes it easy for you to work with MongoDb through a interface named MongoDbContext.
MongoDbContext manages client connection to database. Also, it automatically creates database and collections (with their configuration) based on your models.

Getting Started
---------------

## 1.
## 2. appsettings.json
Add MongoSettings section to appsettings.json:
```
{
  "MongoSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "Database": "TestDb"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

## 3. Models and configurations

You should create your own models like this:

```C#
using MongoDB.Driver;
using MongoDriver.Facade;

namespace Test
{
    // Your collection
    public class Book : MongoDbCollection
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }

    // Optional
    // Applying configuration for the Book collection type.
    // If you don't create this, default collection configuration will be applied.
    public class BookConfigration : ICollectionConfiguration<Book>
    {
        public CreateCollectionOptions Configure()
        {
            return new CreateCollectionOptions { NoPadding = true };
        }
    }
}
```

## 4. MongoDbContext

You can use DbContext like this:

```C#
using MongoDB.Driver;
using MongoDriver.Facade;
using MongoDriver.Facade.TestApi.Models;
using System.Collections.Generic;

namespace TestApi.Services
{
    public class BookService
    {
        private readonly MongoDbContext _dbContext;

        public BookService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Book> Get()
        {
            return _dbContext.Set<Book>().Find(book => true).ToList();
        }

        public Book Get(string id)
        {
            return _dbContext.Set<Book>().Find(book => book.Id == id).FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _dbContext.Set<Book>().InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn)
        {
            _dbContext.Set<Book>().ReplaceOne(book => book.Id == id, bookIn);
        }

        public void Remove(Book bookIn)
        {
            _dbContext.Set<Book>().DeleteOne(book => book.Id == bookIn.Id);
        }

        public void Remove(string id)
        {
            _dbContext.Set<Book>().DeleteOne(book => book.Id == id);
        }
    }
}

```

### Customize DbContext

Also, you can create a custom class that inherited from MongoDbContext then add your customized members:

```C#
 public class ApplicationDbContext : MongoDbContext
    {
        public ApplicationDbContext(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task DropDatabase(string database)
        {
            await Client.DropDatabaseAsync(database);
        }
    }

```

Register MongoDriver.Facade
---------------

```C#
using MongoDriver.Facade;
```

```C#
services.AddMongoDbContext();
```
If there is customized DbContext :

```C#
services.AddMongoDbContext<ApplicationDbContext>();

```
## Give a Star! :star:
If you liked the repo or if it helped you, a star would be appreciated.
