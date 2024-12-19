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
            return Created($"categories/{response.Id}", response);
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
    public async Task<IActionResult> GetActiveAsyncCategory()
    {
        try
        {
            var response = await categoryService.FindByActive();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    } 
    
    [HttpGet("inactive")]
    public async Task<IActionResult> GetInactiveCategory()
    {
        try
        {
            var response = await categoryService.FindByInactive();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoryResponse>> UpdateCategory(int id, CategoryRequest upRequest)
    {
        try
        {
            var response = await categoryService.Update(id, upRequest);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(404, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> DesableActiveCategory(int id)
    {
        try
        {
            var response = await categoryService.FindById(id);
            await categoryService.DesableActiveCategory(id);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(404, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        try
        {
            await categoryService.FindById(id);
            await categoryService.DeleteDisableCategory(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}