using API.Entities;
using API.Enums;

namespace API.Interfaces;

public interface ICategoryService
{
    ValueTask<IEnumerable<Book>> GetCategoryAsync(Category category);
}
