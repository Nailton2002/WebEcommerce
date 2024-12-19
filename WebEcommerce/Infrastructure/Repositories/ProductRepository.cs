using Microsoft.EntityFrameworkCore;
using WebEcommerce.Domain.Entities;
using WebEcommerce.Infrastructure.Data;

namespace WebEcommerce.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext context)
    {
        _dbContext = context;
    }


    public async Task<Product> CreateAsync(Product product)
    {
        _dbContext.Products.Add(product);

        await _dbContext.SaveChangesAsync();

        return product;
    }


    public async Task<Product> FindByIdAsync(int id) =>
        await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id) ??
        throw new InvalidOperationException();


    public async Task<Product> FindByNameAsync(string name)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
    }
    
    
}