using API.Contexts;
using API.Entities;
using API.Models;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtService jwtService;
    private readonly LibraryContext context;
    public AuthController(JwtService jwtService, LibraryContext context)
    {
        this.jwtService = jwtService;
        this.context = context;
    }


    [HttpPost("register")]
    [SwaggerOperation(Summary = "Register a user")]
    public async Task<IActionResult> Register([FromBody]RegisterModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (user != null)
            return BadRequest("User already Exists");

        var newUser = new User
        {
            Id = context.Users.Count() + 1,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Email = model.Email,
            PasswordHash = HashingUtility.HashPassword(model.Password),
            Number = model.Number            
        };

        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();
        return Ok("Registered Successfully");
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Logs in a user and returns jwt token")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (user == null || !HashingUtility.VerifyPassword(model.Password, user!.PasswordHash))
            return Unauthorized("Invalid Credentials");

        var accessToken = jwtService.GenerateToken(user.Id, user.Email, user.Role.ToString());
        return Ok(accessToken);
    }
}
