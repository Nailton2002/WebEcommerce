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
    public async Task<ActionResult<CategoryResponse>> CreateCategory(CategoryRequest request)
    {
        if (false) return BadRequest("Invalid request data.");

        try
        {
            var response = await categoryService.Create(request);

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
    public async Task<ActionResult<List<CategoryResponse>>> GetdAllCategory()
    {
        try
        {
            var response = await categoryService.FindAll();

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryResponse>> GetByIdCategory(int id)
    {
        try
        {
            var response = await categoryService.FindById(id);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(404, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    
    
    [HttpGet("name")]
    public async Task<ActionResult<CategoryResponse>> GetByNameCategory([FromQuery] string name)
    {
        try
        {
            var response = await categoryService.FindByName(name);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    
    [HttpGet("description")]
    public async Task<IActionResult> GetByDescriptionCategory([FromQuery] string description)
    {
        try
        {
            var response = await categoryService.FindByDescription(description);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    } 

    
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveAsync()
    {
        try
        {
            var response = await categoryService.FindActive();

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    } 
    
    [HttpGet("inactive")]
    public async Task<IActionResult> GetInactive()
    {
        try
        {
            var response = await categoryService.FindInactive();

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}