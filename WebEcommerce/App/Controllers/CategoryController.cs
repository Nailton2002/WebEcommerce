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
            var response = await categoryService.CreateCategory(request);
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
    public async Task<ActionResult<List<CategoryResponse>>> GetdAll()
    {
        try
        {
            var response = await categoryService.FindAllCategory();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryResponse>> GetById(int id)
    {
        try
        {
            var response = await categoryService.FindByIdCategory(id);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(404, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    
    
    [HttpGet("name")]
    public async Task<ActionResult<CategoryResponse>> GetByName([FromQuery] string name)
    {
        try
        {
            var response = await categoryService.FindByNameCategory(name);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    
    [HttpGet("description")]
    public async Task<IActionResult> GetByDescription([FromQuery] string description)
    {
        try
        {
            var response = await categoryService.FindByDescriptionCategory(description);
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
            var response = await categoryService.FindByActiveCategory();
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
            var response = await categoryService.FindByInactiveCategory();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoryResponse>> Update(int id, CategoryRequest upRequest)
    {
        try
        {
            var response = await categoryService.UpdateCategory(id, upRequest);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(404, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> DesableActive(int id)
    {
        try
        {
            var response = await categoryService.FindByIdCategory(id);
            await categoryService.DesableActiveCategory(id);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(404, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await categoryService.FindByIdCategory(id);
            await categoryService.DeleteDisableCategory(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}