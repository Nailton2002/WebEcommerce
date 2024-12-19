using WebEcommerce.Domain.Entities;

namespace WebEcommerce.App.DTOs.Response;

public class CategoryResponse
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Active { get; set; }

    // Lista de produtos na resposta, usando DTO para encapsulamento
    public ICollection<ProductResponse>? Products { get; set; }

    
    // Método estático que converte a entidade `Category` para `CategoryResponse`
    public static CategoryResponse FromCategoryToResponse(Category category)
    {
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Active = category.Active,
            Products = category.Products?.Select(ProductResponse.FromProductToResponse).ToList()
        };
    }
}