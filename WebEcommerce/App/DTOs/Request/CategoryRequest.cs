using System.ComponentModel.DataAnnotations;

namespace WebEcommerce.App.DTOs.Request;

public class CategoryRequest
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome da categoria deve ter no máximo 50 caracteres.")]
    public string Name { get; set; }
    
    public string Description { get; set; }
}