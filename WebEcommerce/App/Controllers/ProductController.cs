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
            return Created($"products/{ response.Id }", response);
        }
        catch (ValidationException ex)
        {
            return UnprocessableEntity(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Error = "An unexpected error occurred.",
                Details = ex.Message
            });
        }
    }

    
    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetAllProduct()
    {
        try
        {
            var response = await _productService.FindAllAsync();

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
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
            return StatusCode(500, new
            {
                Error = "An unexpected error occurred.",
                Details = ex.Message
            });
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(int id, ProductRequest request)
    {
        try
        {
            var response = await _productService.UpdateAsync(id, request);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    
}