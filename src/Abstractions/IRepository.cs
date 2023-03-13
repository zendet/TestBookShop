using src.Entities;
using src.Models.DTOs;

namespace src.Abstractions;

public interface IRepositoryService
{
    Task<List<Book>> GetBooksByFilterAsync(GetBookByFilterDto queryOfBook);
    Task<Book?> GetBookByIdAsync(int queryOfBook);
    Task<Book> PostBookAsync(PostBook book);
    Task<Order> PostOrderAsync(int[] bookIds);
    Task<List<Order>?> GetListOfOrderAsync(int numberOfOrder, DateTime dateOfOrder);
}