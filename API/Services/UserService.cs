using API.Contexts;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class UserService : IUserService
{
    private readonly LibraryContext context;
    public UserService(LibraryContext _context) => context = _context;

    public async ValueTask<IEnumerable<User>> GetAllUsers()
    {
        return await context.Users
            .Include(x => x.Loans)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();
    }
}
