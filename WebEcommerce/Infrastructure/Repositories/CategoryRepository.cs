using WebEcommerce.Domain.Entities;
using WebEcommerce.Infrastructure.Data;

namespace WebEcommerce.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Category> CreateAsync(Category category)
    {
        _dbContext.Categories.Add(category);

        await _dbContext.SaveChangesAsync();

        return category;
    }
}