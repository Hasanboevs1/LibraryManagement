using API.Enums;

namespace API.Entities;

public class Author
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public Role Role { get; set; } = Role.Author;
    public ICollection<Book> Books { get; set; }
}