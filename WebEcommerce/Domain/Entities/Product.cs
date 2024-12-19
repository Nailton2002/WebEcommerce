using WebEcommerce.App.DTOs.Request;

namespace WebEcommerce.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public int CategoryId { get; set; }
    
    public Category Category { get; set; }
    
    
    // Método estático que converte DTO ProductRequest para `entidade` `Product`
    public static Product FromRequestToProduct(ProductRequest request)
    {
        return new Product
        {
            Name = request.Name,
            Price = request.Price,
            CategoryId = request.CategoryId
        };
    }
}