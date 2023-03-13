using Microsoft.EntityFrameworkCore;
using TestBookShop.Entities;

namespace TestBookShop;

public class BookShopDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Book> Books { get; set; } = default!;

    public BookShopDbContext(DbContextOptions options) : base(options)
    {
    }
}