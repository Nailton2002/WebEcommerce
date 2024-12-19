using WebEcommerce.Domain.Entities;
using WebEcommerce.Infrastructure.Data;

namespace WebEcommerce.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{

    public async Task<Product> CreateAsync(Product product)
    {
        context.Products.Add(product);

        await context.SaveChangesAsync();

        return product;
    }
}