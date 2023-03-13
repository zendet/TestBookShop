using Mapster;
using Microsoft.EntityFrameworkCore;
using TestBookShop.Abstractions;
using TestBookShop.Entities;
using TestBookShop.Models.DTOs;

namespace TestBookShop.Services;

/// <summary>
/// Simplifies the work with various queries to the database
/// </summary>
public class RepositoryService : IRepositoryService
{
    private readonly BookShopDbContext _dbContext;

    public RepositoryService(BookShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BookDto>> GetBooksByFilterAsync(GetBookByFilterDto queryOfBook)
    {
        return await _dbContext.Books.Where(book =>
            book.Title == queryOfBook.Title && book.DateOfRelease == queryOfBook.DateOfRelease)
            .ProjectToType<BookDto>()
            .ToListAsync();
    }

    public async Task<BookDto?> GetBookByIdAsync(int queryOfBook)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == queryOfBook);
        return book?.Adapt<BookDto>();
    }

    public async Task<BookDto> PostBookAsync(PostBook book)
    {
        var newBook = book.Adapt<Book>();
        
        var postedBook = await _dbContext.Books.AddAsync(newBook);
        await _dbContext.SaveChangesAsync();
        return postedBook.Entity.Adapt<BookDto>();
    }

    public async Task<OrderDto> PostOrderAsync(int[] bookIds)
    {
        var books = new List<Book>();

        foreach (var bookId in bookIds)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == bookId);
            if (book is not null)
                books.Add(book);
        }
        
        var order = new Order { Books = books, TimeOfOrder = DateTime.Now};

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order.Adapt<OrderDto>();
    }

    public async Task<List<OrderDto>?> GetListOfOrderAsync(int numberOfOrder, DateTime dateOfOrder)
    {
        return await _dbContext.Orders.Where(order =>
            order.OrderNumber == numberOfOrder && order.TimeOfOrder == dateOfOrder)
            .ProjectToType<OrderDto>()
            .ToListAsync();
    }
}