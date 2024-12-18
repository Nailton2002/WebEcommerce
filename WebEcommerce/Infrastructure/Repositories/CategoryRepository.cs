using Microsoft.EntityFrameworkCore;
using WebEcommerce.Domain.Entities;
using WebEcommerce.Infrastructure.Data;

namespace WebEcommerce.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> CreateAsync(Category category)
    {
        dbContext.Categories.Add(category);

        await dbContext.SaveChangesAsync();

        return category;
    }
    
    public async Task<IEnumerable<Category>> FindAllAsync() => await dbContext.Categories.ToListAsync();
    
    public async Task<Category> FindByIdAsync(int id) => await dbContext.Categories.FindAsync(id) ?? throw new InvalidOperationException();
    
    public async Task<IEnumerable<Category>> FindByNameAsync(string name) => await dbContext.Categories
            .Where(u => u.Name.Contains(name)) // Filtra categoria por nome
            .ToListAsync();
}