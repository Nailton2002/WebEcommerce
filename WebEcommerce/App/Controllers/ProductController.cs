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
    public async Task<ActionResult<ProductResponse>> Create(ProductRequest request)
    {
        if (request == null)
            return BadRequest(new { Error = "dados de solicitação inválidos." });

        try
        {
            var response = await _productService.CreateProduct(request);
            return Created($"products/{response.Id}", response);
        }
        catch (ValidationException ex)
        {
            return UnprocessableEntity(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Error = "Ocorreu um erro inesperado.",
                Details = ex.Message
            });
        }
    }


    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetAll()
    {
        try
        {
            var response = await _productService.FindAllProduct();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Ocorreu um erro inesperado.", Details = ex.Message });
        }
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponse>> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { Error = "O ID fornecido deve ser maior que zero." });
        }

        try
        {
            var response = await _productService.FindByIdProduct(id);
            if (response == null)
            {
                return NotFound(new { Error = $"Nenhum produto encontrado com ID {id}." });
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Error = "Ocorreu um erro inesperado.",
                Details = ex.Message
            });
        }
    }
    
    
    [HttpGet("name")]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        try
        {
            var products = await _productService.FindByNameProduct(name);
            return Ok(products);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro de entrada: {ex.Message}");
            return BadRequest(new { Error = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Produto não encontrado: {ex.Message}");
            return NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            return StatusCode(500, new { Error = "Ocorreu um erro inesperado.", Details = ex.Message });
        }
    }


    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponse>> Update(int id, ProductRequest request)
    {
        if (id <= 0) return BadRequest(new { Error = "O ID fornecido deve ser maior que zero." });

        try
        {
            var response = await _productService.UpdateProduct(id, request);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Ocorreu um erro inesperado.", Details = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest(new { Error = "O ID fornecido deve ser maior que zero." });

        try
        {
            var response = await _productService.FindByIdProduct(id);
            if (response == null)
            {
                return NotFound();
            }
            await _productService.DeleteProduct(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Ocorreu um erro inesperado.", Details = ex.Message });
        }
    }
}