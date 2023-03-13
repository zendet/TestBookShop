using TestBookShop.Entities;
using TestBookShop.Models.DTOs;

namespace TestBookShop.Abstractions;

public interface IRepositoryService
{
    Task<List<BookDto>> GetBooksByFilterAsync(GetBookByFilterDto queryOfBook);
    Task<BookDto?> GetBookByIdAsync(int queryOfBook);
    Task<BookDto> PostBookAsync(PostBook book);
    Task<OrderDto> PostOrderAsync(int[] bookIds);
    Task<List<OrderDto>?> GetListOfOrderAsync(int numberOfOrder, DateTime dateOfOrder);
}