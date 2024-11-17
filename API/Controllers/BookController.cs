using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[Route("api/books")]
[ApiController]
[Authorize(Roles = "Author, Manager")]
public class BookController : ControllerBase
{
    private readonly IBookService _service;
    public BookController(IBookService service) => _service = service;

    [HttpGet]
    [SwaggerOperation(Summary = "Get All Books")]
    public async Task<IActionResult> GetBooks() => Ok(await _service.GetBooks());

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get book by Id")]
    public async Task<IActionResult> GetBook(int id) => Ok(await _service.GetBook(id));

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete a book by Id")]
    public async Task<IActionResult> DeleteBook(int id) => Ok(await _service.DeleteBook(id));

    [HttpPost("{userId:int}")]
    [SwaggerOperation(Summary = "Add a book")]
    public async Task<IActionResult> AddBook(int userId, [FromForm] BookModel book) => Ok(await _service.AddBook(userId, book));

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update a book")]
    public async Task<IActionResult> UpdateBook(int id, [FromForm] BookModel model) => Ok(await _service.UpdateBook(id, model));

}
