using Microsoft.AspNetCore.Mvc;
using MongoDriver.Facade.TestApi.Models;
using MongoDriver.Facade.TestApi.Services;
using System.Collections.Generic;

namespace MongoDriver.Facade.TestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public ActionResult<List<Book>> Get()
    {
        return _bookService.Get();
    }
}
