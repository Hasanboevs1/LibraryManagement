using API.Entities;

namespace API.Interfaces;

public interface ILoanService
{
    Task<Guid?> BorrowBookAsync(int bookId, int userId);
    Task<bool> ReturnBookAsync(Guid code);
    Task<IEnumerable<Loan>> GetAllLoans();
}