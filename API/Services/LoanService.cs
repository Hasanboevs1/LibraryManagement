using API.Contexts;
using API.Entities;
using API.Exceptions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class LoanService : ILoanService
{
    private readonly LibraryContext _context;

    public LoanService(LibraryContext context) => _context = context;

    public async Task<Guid?> BorrowBookAsync(int bookId, int userId)
    {
        var book = await _context.Books
            .Include(b => b.Loans)
            .FirstOrDefaultAsync(b => b.Id == bookId);

        if (book == null || book.BookCount <= 0 || !book.IsAvailable)
            throw new LibraryException(404, "Book_is_not_available");

        var loan = new Loan
        {
            BookId = bookId,
            UserId = userId,
            LoanDate = DateTime.UtcNow,
            ReturnDate = DateTime.UtcNow.AddDays(7) 
        };

        await _context.Loans.AddAsync(loan);

        book.BookCount -= 1;

        if (book.BookCount == 0)
        {
            book.IsAvailable = false;
        }

        await _context.SaveChangesAsync();
        return GuidHelper.IntToGuid(loan.Id);
    }

    public async Task<bool> ReturnBookAsync(Guid code)
    {
        int loanId = GuidHelper.GuidToInt(code);
        var loan = await _context.Loans
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.Id == loanId);

        if (loan == null)
        {
            return false;
        }

        loan.ReturnDate = DateTime.UtcNow;

        var book = loan.Book;
        book.BookCount += 1;

        if (book.BookCount > 0)
        {
            book.IsAvailable = true;
        }

        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Loan>> GetAllLoans()
    {
        return await _context.Loans.AsNoTracking().ToListAsync();
    }
}