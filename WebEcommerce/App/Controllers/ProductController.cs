using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;
using WebEcommerce.Domain.Services;

namespace WebEcommerce.App.Controllers;

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct(ProductRequest request)
    {
        if (request == null)
            return BadRequest(new { Error = "Invalid request data." });

        try
        {
            var response = await _productService.CreateAsync(request);

            // Retorna a rota correta usando nameof e o ID do recurso criado
            return CreatedAtAction(nameof(CreateProduct), new { id = response.Id }, response);
        }
        catch (ValidationException ex)
        {
            // Retorna erro de validação com mensagem detalhada
            return UnprocessableEntity(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            // Retorna erro genérico com mensagem de detalhe para depuração
            return StatusCode(500, new
            {
                Error = "An unexpected error occurred.",
                Details = ex.Message
            });
        }
    }
}