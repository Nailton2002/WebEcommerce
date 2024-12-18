using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;
using WebEcommerce.Domain.Entities;
using WebEcommerce.Infrastructure.Repositories;

namespace WebEcommerce.Domain.Services;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task<CategoryResponse> Create(CategoryRequest request)
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


    public async Task<IEnumerable<CategoryResponse>> FindAll()
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

    
    public async Task<CategoryResponse> FindById(int id)
    {
        try
        {
            var category = await repository.FindByIdAsync(id);


            return CategoryResponse.FromCategoryToResponse(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    
    public async Task<IEnumerable<CategoryResponse>> FindByName(string name)
    {
        try
        {
            var categories = await repository.FindByNameAsync(name);

            return categories.Select(CategoryResponse.FromCategoryToResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

  
    public async Task<IEnumerable<CategoryResponse>> FindByDescription(string description)
    {
        try
        {
            var categories = await repository.FindByDescriptionAsync(description);

            return categories.Select(CategoryResponse.FromCategoryToResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<IEnumerable<CategoryResponse>> FindActive()
    {
        try
        {
            var categories = await repository.FindByActiveAsync();

            return categories.Select(CategoryResponse.FromCategoryToResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}