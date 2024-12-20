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

    
    public async Task<IEnumerable<Product>> FindAllAsync() =>
        await _dbContext.Products.Include(p => p.Category).ToListAsync();


    public async Task<Product> FindByIdAsync(int id) =>
        await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id) ??
        throw new InvalidOperationException();


    public async Task<IEnumerable<Product>> FindByNameAsync(string name)
    {
        try
        {
            return await _dbContext.Products
                .Include(p => p.Category) // Carrega a categoria relacionada
                .Where(u => u.Name.Contains(name))
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao buscar produtos no banco de dados.", ex);
        }
    }



    public async Task<Product> SearchForSameNames(string name)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
    }
    
    
    public async Task<Product> UpdateAsync(Product product)
    {
        if (product.Id != 0) _dbContext.Products.Update(product);

        await _dbContext.SaveChangesAsync();

        return product;
    }
    

    public async Task DeleteAsync(Product product)
    {
        _dbContext.Products.Remove(product);

        await _dbContext.SaveChangesAsync();
    }
}