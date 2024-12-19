using System.ComponentModel.DataAnnotations;
using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;
using WebEcommerce.Domain.Entities;
using WebEcommerce.Infrastructure.Repositories;

namespace WebEcommerce.Domain.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    public async Task<ProductResponse> CreateAsync(ProductRequest request)
    {
        try
        {
            var product = Product.FromRequestToProduct(request);
            var createdProduct = await repository.CreateAsync(product);
            return ProductResponse.FromProductToResponse(createdProduct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}