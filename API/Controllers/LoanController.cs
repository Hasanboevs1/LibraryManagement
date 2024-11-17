using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[Route("api/laons")]
[ApiController]
[Authorize(Roles = "User")]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService) => _loanService = loanService;

    [HttpPost("borrow/{bookId:int}/{userId:int}")]
    [SwaggerOperation(Summary = "Borrow a Book",
    Description = "You can get a book for 7 days at most and it should be returned. Do not lose the code, it is needed to return the book")]
    public async Task<IActionResult> BorrowBook(int bookId, int userId)
    {
        var result = await _loanService.BorrowBookAsync(bookId, userId);
        if (result != null)
        {
            return Ok(new { message = "Book borrowed successfully.", Code = result });
        }
        return BadRequest(new { message = "Failed to borrow the book. It may be out of stock or unavailable." });
    }

    [HttpPost("return/{code:Guid}")]
    [SwaggerOperation(Summary = "Return a borrowed Book")]
    public async Task<IActionResult> ReturnBook(Guid code)
    {
        var result = await _loanService.ReturnBookAsync(code);
        if (result)
        {
            return Ok(new { message = "Book returned successfully." });
        }
        return BadRequest(new { message = "Failed to return the book. Loan not found." });
    }

    [HttpGet]
    [SwaggerOperation(Summary = "All Loans")]
    public async Task<IActionResult> GetAll() => Ok(await _loanService.GetAllLoans());

}