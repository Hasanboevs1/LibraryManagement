using API.Contexts;
using API.Entities;
using API.Exceptions;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class AuthorService : IAuthorService
{
    private readonly LibraryContext _context;

    public AuthorService(LibraryContext context) => _context = context;

    public async ValueTask<Author> CreateAsync(AuthorModel model)
    {
        var author = new Author
        {
            Firstname = model.Firstname,
            Lastname = model.Lastname,
        };

        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (author == null)
            throw new LibraryException(404, "author_not_found");

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors
            .Include(x => x.Books)
            .ThenInclude(x => x.Loans)
            .Include(x => x.Books)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();
    }

    public async ValueTask<Author> GetAsync(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (author == null)
            throw new LibraryException(404, "author_not_found");

        return author;
    }

    public async ValueTask<Author> UpdateAsync(int id, AuthorModel model)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (author == null)
            throw new LibraryException(404, "author_not_found");

        author.Firstname = model.Firstname;
        author.Lastname = model.Lastname;

        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
        return author;
    }
}
