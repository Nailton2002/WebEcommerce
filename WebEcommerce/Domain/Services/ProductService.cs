using System.ComponentModel.DataAnnotations;
using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;
using WebEcommerce.Domain.Entities;
using WebEcommerce.Infrastructure.Repositories;

namespace WebEcommerce.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> CreateAsync(ProductRequest request)
    {
        try
        {
            // Verifica se o nome do produto já existe
            var existingProduct = await _repository.FindByNameAsync(request.Name);
            //Se já existe lança uma exceção
            if (existingProduct != null)
            {
                throw new ValidationException($"O produto com o nome '{request.Name}' já existe.");
            }

            var product = Product.FromRequestToProduct(request);
            var createdProduct = await _repository.CreateAsync(product);
            return ProductResponse.FromProductToResponse(createdProduct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<ProductResponse>> FindAllAsync()
    {
        try
        {
            var products = await _repository.FindAllAsync();
            return products.Select(product => ProductResponse.FromProductToResponse(product));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ProductResponse> FindByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("O ID deve ser maior que zero.", nameof(id));
        }

        try
        {
            var product = await _repository.FindByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Produto com ID {id} não foi encontrado.");
            }

            return ProductResponse.FromProductToResponse(product);
        }
        catch (Exception ex)
        {
            // Opcional: registrar o erro para diagnóstico
            Console.Error.WriteLine($"Erro ao buscar produto com ID {id}: {ex.Message}");
            throw; // Re-lança a exceção para ser tratada em níveis superiores
        }
    }


    public async Task<ProductResponse> UpdateAsync(int id, ProductRequest upRequest)
    {
        if (id <= 0) throw new ArgumentException("O ID deve ser maior que zero.", nameof(id));
        try
        {
            var product = await _repository.FindByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");
            product.UpdateProduct(upRequest);
            var upProduct = await _repository.UpdateAsync(product);
            return ProductResponse.FromProductToResponse(upProduct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}