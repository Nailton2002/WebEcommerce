using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;
using WebEcommerce.Domain.Entities;
using WebEcommerce.Infrastructure.Repositories;

namespace WebEcommerce.Domain.Services;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task<CategoryResponse> CreateAsync(CategoryRequest request)
    {
        try
        {
            var category = Category.FromRequestToCategory(request);

            var createCategory = await repository.CreateAsync(category);

            return CategoryResponse.FromCategoryToResponse(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<IEnumerable<CategoryResponse>> FindAllAsync()
    {
        try
        {
            var categories = await repository.FindAllAsync();

            return categories.Select(CategoryResponse.FromCategoryToResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


 
}