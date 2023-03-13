using Microsoft.AspNetCore.Mvc;
using src.Abstractions;
using src.Entities;
using src.Exceptions;
using src.Models.DTOs;

namespace src.Controllers;

/// <summary>
/// Controller for getting/posting books
/// </summary>
[ApiController] [Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IRepositoryService _repository;

    public BooksController(IRepositoryService repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetBooksByFilter([FromQuery] GetBookByFilterDto bookByFilter)
    {
        var books = await _repository.GetBooksByFilterAsync(bookByFilter);
        if (books.Capacity == 0)
            throw new NotFoundException(
                $"A books with title \"{bookByFilter.Title}\" or date {bookByFilter.DateOfRelease:s} cannot be found.");
        
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetBookById([FromRoute] int id)
    {
        var book = await _repository.GetBookByIdAsync(id);

        if (book is null)
            throw new NotFoundException($"A book with ID: {id} cannot be found.");
        
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(PostBook book)
    {
        var postedBook = await _repository.PostBookAsync(book);
        return Ok(postedBook);
    }
}