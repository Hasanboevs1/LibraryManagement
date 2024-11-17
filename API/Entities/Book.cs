using API.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public Category Category { get; set; }
    public int BookCount { get; set; }
    public int AuthorId { get; set; }
    [JsonIgnore]
    public Author Author { get; set; }
    public ICollection<Loan> Loans { get; set; }
    public bool IsAvailable { get; set; }
}