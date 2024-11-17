using API.Enums;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service) => _service = service;

    [HttpGet]
    [SwaggerOperation(Summary = "Category")]
    public async Task<IActionResult> Get([FromForm] Category category)
    {
        if (!Enum.IsDefined(typeof(Category), category))
        {
            return BadRequest("Invalid category");
        }

        var books = await _service.GetCategoryAsync(category);
        return Ok(books);
    }
}
