using Microsoft.EntityFrameworkCore;
using src.Abstractions;
using src.Entities;
using src.Models.DTOs;

namespace src.Services;

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

    public async Task<List<Book>> GetBooksByFilterAsync(GetBookByFilterDto queryOfBook)
    {
        return await _dbContext.Books.Where(book =>
            book.Title == queryOfBook.Title && book.DateOfRelease == queryOfBook.DateOfRelease).ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int queryOfBook)
    {
        return await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == queryOfBook);
    }

    public async Task<Book> PostBookAsync(PostBook book)
    {
        var newBook = new Book
        {
            Title = book.Title,
            DateOfRelease = book.DateOfRelease
        };
        
        var postedBook = await _dbContext.Books.AddAsync(newBook);
        await _dbContext.SaveChangesAsync();
        return postedBook.Entity;
    }

    public async Task<Order> PostOrderAsync(int[] bookIds)
    {
        var books = new List<Book>();

        foreach (var bookId in bookIds)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == bookId);
            if (book is not null)
                books.Add(book);
        }
        
        var order = new Order { Books = books };

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task<List<Order>?> GetListOfOrderAsync(int numberOfOrder, DateTime dateOfOrder)
    {
        return await _dbContext.Orders.Where(order =>
            order.OrderNumber == numberOfOrder && order.TimeOfOrder == dateOfOrder).ToListAsync();
    }
}