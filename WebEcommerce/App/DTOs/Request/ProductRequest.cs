using System.ComponentModel.DataAnnotations;

namespace WebEcommerce.App.DTOs.Request;

public class ProductRequest
{
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "A categoria do produto é obrigatória.")]
    public int CategoryId { get; set; }
}