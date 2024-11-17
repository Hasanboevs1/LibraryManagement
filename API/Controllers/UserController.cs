using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;
[ApiController]
[Route("api/users")]
[Authorize(Roles = "Manager")]
public class UserController: ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService service) => _service = service;

    [HttpGet]
    [SwaggerOperation(Summary = "Get Users")]
    public async ValueTask<IActionResult> GetUsers()
        => Ok(await _service.GetAllUsers());
}
