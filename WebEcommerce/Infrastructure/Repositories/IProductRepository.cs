using WebEcommerce.Domain.Entities;

namespace WebEcommerce.Infrastructure.Repositories;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product);
}