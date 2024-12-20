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

    public async Task<ProductResponse> CreateProduct(ProductRequest request)
    {
        try
        {
            var existingProduct = await _repository.SearchForSameNames(request.Name);
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

    public async Task<IEnumerable<ProductResponse>> FindAllProduct()
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

    public async Task<IEnumerable<ProductResponse>> FindByNameProduct(string name)
    {
        try
        {
            // Validação do nome (regra de negócio)
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(name));
            }

            // Consulta ao repositório
            var products = await _repository.FindByNameAsync(name);

            // Regra de negócio: garantir que a lista não seja nula
            if (products == null || !products.Any())
            {
                throw new KeyNotFoundException($"Nenhum produto encontrado com o nome: {name}");
            }

            // Converte para DTO (resposta)
            return products
                .Where(product => product != null) // Remove produtos nulos
                .Select(ProductResponse.FromProductToResponse)
                .ToList();
        }
        catch (ArgumentException ex)
        {
            // Log para validação de entrada
            Console.WriteLine($"Validação de entrada falhou: {ex.Message}");
            throw;
        }
        catch (KeyNotFoundException ex)
        {
            // Log para ausência de dados
            Console.WriteLine($"Nenhum produto encontrado: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            // Log para erros inesperados
            Console.WriteLine($"Erro inesperado no serviço: {ex.Message}");
            throw;
        }
    }



    public async Task<ProductResponse> FindByIdProduct(int id)
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


    public async Task<ProductResponse> UpdateProduct(int id, ProductRequest upRequest)
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

    public async Task DeleteProduct(int id)
    {
        if (id <= 0) throw new ArgumentException("O ID deve ser maior que zero.", nameof(id));
        try
        {
            var product = await _repository.FindByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");
            await _repository.DeleteAsync(product);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}