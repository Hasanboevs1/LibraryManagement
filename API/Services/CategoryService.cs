using API.Contexts;
using API.Entities;
using API.Enums;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class CategoryService : ICategoryService
{
    private readonly LibraryContext _context;   
    public async ValueTask<IEnumerable<Book>> GetCategoryAsync(Category category)
    {
        return await _context.Books
                         .Where(x => x.Category == category)
                         .ToListAsync();
    }
}
