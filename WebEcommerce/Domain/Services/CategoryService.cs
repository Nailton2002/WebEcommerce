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

    public async Task<IEnumerable<CategoryResponse>> FindByActive()
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

    public async Task<IEnumerable<CategoryResponse>> FindByInactive()
    {
        try
        {
            var categories = await repository.FindByInactiveAsync();
            return categories.Select(CategoryResponse.FromCategoryToResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CategoryResponse> Update(int id, CategoryRequest upRequest)
    {
        try
        {
            // Busca a categoria pelo ID
            var category = await repository.FindByIdAsync(id);
            //Se não existir, retorna uma exceção de chave não encontrada 
            if (category == null) throw new KeyNotFoundException("Category not found");
            //Pegando os dados para atualizar
            category.UpdateCategory(upRequest);
            // Atualiza a categoria no repositório
            var upCategory = await repository.UpdateAsync(category);
            // Converte a entidade atualizada em uma resposta
            return CategoryResponse.FromCategoryToResponse(upCategory);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CategoryResponse> DesableActiveCategory(int id)
    {
        try
        {
            var category = await repository.FindByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            //Passa a ativação de verdadeira para falsa
            category.DesableActiveCategory();
            // Atualizada os dados referentes
            await repository.UpdateAsync(category);
            // Converte a entidade atualizada em uma resposta
            return CategoryResponse.FromCategoryToResponse(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task DeleteDisableCategory(int id)
    {
        try
        {
            var category = await repository.FindByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            if (category.Active)
            {
                throw new Exception("Category is active and cannot be deleted");
            }
            await repository.DeleteAsync(category);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}