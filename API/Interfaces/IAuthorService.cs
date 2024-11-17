using API.Entities;
using API.Models;

namespace API.Interfaces;

public interface IAuthorService
{
    ValueTask<Author> CreateAsync(AuthorModel model);
    ValueTask<Author> UpdateAsync(int id, AuthorModel model);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<Author> GetAsync(int id);
    ValueTask<IEnumerable<Author>> GetAllAsync();
}
