using WebEcommerce.Domain.Entities;

namespace WebEcommerce.Infrastructure.Repositories;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product);
    
    Task<Product> FindByIdAsync(int id);

    Task<Product> FindByNameAsync(string name);
}