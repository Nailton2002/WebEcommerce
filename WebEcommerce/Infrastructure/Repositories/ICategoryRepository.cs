using WebEcommerce.Domain.Entities;

namespace WebEcommerce.Infrastructure.Repositories;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
}