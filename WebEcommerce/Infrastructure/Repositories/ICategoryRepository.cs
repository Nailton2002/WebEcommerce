using WebEcommerce.Domain.Entities;

namespace WebEcommerce.Infrastructure.Repositories;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
    
    Task<IEnumerable<Category>> FindAllAsync();
    
    Task<Category> FindByIdAsync(int id);
    
    Task<IEnumerable<Category>> FindByNameAsync(string name);
    
    Task<IEnumerable<Category>> FindByDescriptionAsync(string description);
}