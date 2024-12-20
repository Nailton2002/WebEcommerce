using WebEcommerce.Domain.Entities;

namespace WebEcommerce.App.DTOs.Response;

public class ProductResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string CategoryName { get; set; }


    public static ProductResponse FromProductToResponse(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product), "O produto não pode ser nulo.");

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name ?? "Nome não informado", // Validação para evitar referência nula
            Price = product.Price, // Certifique-se que o preço está inicializado
            CategoryName = product.Category?.Name ?? "Categoria não informada" // Validação para evitar nulo
        };
    }
}