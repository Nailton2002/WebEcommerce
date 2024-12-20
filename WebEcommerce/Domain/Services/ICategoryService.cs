using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;

namespace WebEcommerce.Domain.Services;

public interface ICategoryService
{
    Task<CategoryResponse> CreateCategory(CategoryRequest request);

    Task<IEnumerable<CategoryResponse>> FindAllCategory();
    
    Task<CategoryResponse> FindByIdCategory(int id);

    Task<IEnumerable<CategoryResponse>> FindByNameCategory(string name);
    
    Task<IEnumerable<CategoryResponse>> FindByDescriptionCategory(string description);
    
    Task<IEnumerable<CategoryResponse>> FindByActiveCategory();
    
    Task<IEnumerable<CategoryResponse>> FindByInactiveCategory();
    
    Task<CategoryResponse> UpdateCategory(int id, CategoryRequest upRequest);
    
    Task<CategoryResponse>  DesableActiveCategory(int id);
    
    Task DeleteDisableCategory(int id);
}