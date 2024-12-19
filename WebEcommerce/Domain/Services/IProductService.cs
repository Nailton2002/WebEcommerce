using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;

namespace WebEcommerce.Domain.Services;

public interface IProductService
{
    Task<ProductResponse> CreateAsync(ProductRequest request);

    Task<IEnumerable<ProductResponse>> FindAllAsync();
    
    Task<ProductResponse> FindByIdAsync(int id);

    Task<ProductResponse> UpdateAsync(int id, ProductRequest request);

    Task DeleteAsync(int id);
}