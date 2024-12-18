using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebEcommerce.App.DTOs.Request;
using WebEcommerce.App.DTOs.Response;
using WebEcommerce.Domain.Services;

namespace WebEcommerce.App.Controllers;

[ApiController]
[Route("/categories")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CategoryResponse>> Create(CategoryRequest request)
    {
        if (false) return BadRequest("Invalid request data.");

        try
        {
            var response = await categoryService.CreateAsync(request);

            return Created($"api/categories/{response.Id}", response);
        }
        catch (ValidationException ex)
        {
            return UnprocessableEntity(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CategoryResponse>>> FindAll()
    {
        try
        {
            var response = await categoryService.FindAllAsync();

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    

    
    
}