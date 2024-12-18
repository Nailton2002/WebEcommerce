using System.Text.Json.Serialization;
using WebEcommerce.App.DTOs.Request;

namespace WebEcommerce.Domain.Entities;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Descricao { get; set; }

    public bool Active { get; set; }
    
    // Método estático que converte a CategoryRequest `Category` para `entidade`
    public static Category FromRequestToCategory(CategoryRequest request)
    {
        return new Category
        {
            Name = request.Name,
            Descricao = request.Descricao,
            Active = true
        };
    }

}