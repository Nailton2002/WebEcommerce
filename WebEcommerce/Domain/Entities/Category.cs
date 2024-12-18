using System.Text.Json.Serialization;
using WebEcommerce.App.DTOs.Request;

namespace WebEcommerce.Domain.Entities;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Active { get; set; }
    
    
    // Método estático que converte a CategoryRequest `Category` para `entidade`
    public static Category FromRequestToCategory(CategoryRequest request)
    {
        return new Category
        {
            Name = request.Name,
            Description = request.Description,
            Active = true
        };
    }

    public void UpdateCategory(CategoryRequest upRequest)
    {
        if (upRequest.Name != null) Name = upRequest.Name;
        if (upRequest.Description != null) Description = upRequest.Description;
    }
    
    public void DesableActiveCategory()
    {
        Active = false;
    }
}