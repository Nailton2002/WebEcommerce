using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;

namespace WebEcommerce.Domain.Services;

public interface ICategoryService
{
    Task<CategoryResponse> CreateAsync(CategoryRequest request);
}