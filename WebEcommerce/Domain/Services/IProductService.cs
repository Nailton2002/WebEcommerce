using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;

namespace WebEcommerce.Domain.Services;

public interface IProductService
{
    Task<ProductResponse> CreateAsync(ProductRequest request);
}