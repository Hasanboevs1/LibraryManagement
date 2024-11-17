using API.Entities;
using API.Models;

namespace API.Interfaces;

public interface IBookService
{
    ValueTask<Book> GetBook(int id);
    ValueTask<IEnumerable<Book>> GetBooks();
    ValueTask<bool> DeleteBook(int id);
    ValueTask<Book> AddBook(int authorId, BookModel dto);
    ValueTask<Book> UpdateBook(int id, BookModel dto);
}

