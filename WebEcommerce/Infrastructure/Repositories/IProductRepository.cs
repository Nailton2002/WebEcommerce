using WebEcommerce.Domain.Entities;

namespace WebEcommerce.Infrastructure.Repositories;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product);
    
    Task<IEnumerable<Product>> FindAllAsync();
    
    Task<Product> FindByIdAsync(int id);

    Task<IEnumerable<Product>> FindByNameAsync(string name);
    
    Task<Product> SearchForSameNames(string name);

    Task<Product> UpdateAsync(Product updatedProduct);

    Task DeleteAsync(Product product);
}