using API.Entities;

namespace API.Interfaces;

public interface IUserService
{
    ValueTask<IEnumerable<User>> GetAllUsers();
}
