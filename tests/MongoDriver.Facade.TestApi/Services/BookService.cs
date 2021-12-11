using MongoDB.Driver;
using MongoDriver.Facade.TestApi.Models;
using System.Collections.Generic;

namespace MongoDriver.Facade.TestApi.Services;

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
