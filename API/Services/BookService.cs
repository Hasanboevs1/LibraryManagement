using API.Contexts;
using API.Entities;
using API.Exceptions;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class BookService : IBookService
{
    private readonly LibraryContext context;
    public BookService(LibraryContext context)
    {
        this.context = context;
    }

    public async ValueTask<Book> AddBook(int authorId, BookModel dto)
    {
        var book = await context.Books.FirstOrDefaultAsync(x => x.ISBN == dto.ISBN && x.AuthorId == authorId);
        var author = await context.Authors.FirstOrDefaultAsync(x => x.Id == authorId);
        if (book != null && author != null)
            throw new LibraryException(401, "book_already_exists");
        if (author == null)
            throw new LibraryException(404, "author_not_found");

        var newBook = new Book
        { 
           AuthorId = authorId,
           Title = dto.Title,
           ISBN = dto.ISBN,
           IsAvailable = true,
           Category = dto.Category,
           BookCount = dto.BookCount
        };

        await context.Books.AddAsync(newBook);
        await context.SaveChangesAsync();
        return newBook;
    }

    public async ValueTask<bool> DeleteBook(int id)
    {
        var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book == null)
            throw new LibraryException(404, "book_not_found");
        context.Books.Remove(book);
        await context.SaveChangesAsync();
        return true;
    }

    public async ValueTask<Book> GetBook(int id)
    {
        var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book == null)
            throw new LibraryException(404, "book_not_found");
        return book;
    }

    public async ValueTask<IEnumerable<Book>> GetBooks()
    {
        return await context.Books
            .Include(x => x.Loans)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();
    }

    public async ValueTask<Book> UpdateBook(int id, BookModel dto)
    {
        var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book == null)
            throw new LibraryException(404, "book_not_found");

        book.Title = dto.Title;
        book.ISBN = dto.ISBN;
        book.BookCount = dto.BookCount;
        book.Category = dto.Category;

        context.Books.Update(book);
        await context.SaveChangesAsync();
        return book;
    }
}
