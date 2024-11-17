using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;


[ApiController]
[Route("api/authors")]
[Authorize(Roles = "Author, Manager")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;
    public AuthorController(IAuthorService service) => _service = service;

    [HttpGet]
    [SwaggerOperation(Summary = "Get All Authors")]
    public async Task<IActionResult> GetAuthors() => Ok(await _service.GetAllAsync());


    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get Author by Id")]
    public async Task<IActionResult> GetAuthor(int id) => Ok(await _service.GetAsync(id));

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Remove Author by Id")]
    public async Task<IActionResult> DeleteAuthor(int id) => Ok(await _service.DeleteAsync(id));

    [HttpPost]
    [SwaggerOperation(Summary = "Create new Author",Description = "It requires firstname and lastname and creates new Author who can crete books")]
    public async Task<IActionResult> AddAuthor([FromBody] AuthorModel model) => Ok(await _service.CreateAsync(model));

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update Author")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorModel model) => Ok(await _service.UpdateAsync(id, model));

}
