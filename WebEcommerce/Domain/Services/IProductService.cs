using Microsoft.AspNetCore.Mvc;
using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;

namespace WebEcommerce.Domain.Services;

public interface IProductService
{
    Task<ProductResponse> CreateProduct(ProductRequest request);

    Task<IEnumerable<ProductResponse>> FindAllProduct();
    
    Task<IEnumerable<ProductResponse>> FindByNameProduct(string name);
    
    Task<ProductResponse> FindByIdProduct(int id);

    Task<ProductResponse> UpdateProduct(int id, ProductRequest request);

    Task DeleteProduct(int id);
    
   
}