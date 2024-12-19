using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;

namespace WebEcommerce.Domain.Services;

public interface ICategoryService
{
    Task<CategoryResponse> Create(CategoryRequest request);

    Task<IEnumerable<CategoryResponse>> FindAll();
    
    Task<CategoryResponse> FindById(int id);

    Task<IEnumerable<CategoryResponse>> FindByName(string name);
    
    Task<IEnumerable<CategoryResponse>> FindByDescription(string description);
    
    Task<IEnumerable<CategoryResponse>> FindByActive();
    
    Task<IEnumerable<CategoryResponse>> FindByInactive();
    
    Task<CategoryResponse> Update(int id, CategoryRequest upRequest);
    
    Task<CategoryResponse>  DesableActiveCategory(int id);
    
    Task DeleteDisableCategory(int id);
}