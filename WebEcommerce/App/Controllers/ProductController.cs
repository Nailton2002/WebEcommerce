using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;
using WebEcommerce.Domain.Services;

namespace WebEcommerce.App.Controllers;

[ApiController]
[Route("products")]
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
            return Created($"products/{ response.Id }", response);
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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponse>> GetByIdProduct(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { Error = "The provided ID must be greater than 0." });
        }
        try
        {
            var response = await _productService.FindByIdAsync(id);
            if (response == null)
            {
                return NotFound(new { Error = $"No product found with ID {id}." });
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            // Retorna erro genérico com mensagem detalhada para desenvolvimento, pode ser ajustado para produção
            return StatusCode(500, new
            {
                Error = "An unexpected error occurred.",
                Details = ex.Message
            });
        }
    }
}