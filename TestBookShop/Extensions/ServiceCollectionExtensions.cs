using Microsoft.EntityFrameworkCore;
using TestBookShop.Abstractions;
using TestBookShop.Services;

namespace TestBookShop.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddScoped<IRepositoryService, RepositoryService>()
            .AddScoped<ExceptionMiddlewareService>();
    
    public static IServiceCollection AddPersistense(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<BookShopDbContext>(options => 
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 31)) ??
                                 throw new InvalidOperationException("DEFAULT_CONNECTION variable isn't set.")));
}