using Microsoft.EntityFrameworkCore;
using src.Entities;

namespace src;

public class BookShopDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Book> Books { get; set; } = default!;

    public BookShopDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
}