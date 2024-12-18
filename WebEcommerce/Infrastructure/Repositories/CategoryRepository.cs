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
    
    
}