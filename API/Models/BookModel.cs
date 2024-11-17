using API.Enums;

namespace API.Models;

public class BookModel
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public Category Category { get; set; }
    public int BookCount { get; set; }

}
