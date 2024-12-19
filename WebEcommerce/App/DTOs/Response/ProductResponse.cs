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
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryName = product.Category.Name
        };
    }
}