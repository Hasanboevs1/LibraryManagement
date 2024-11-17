using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId);

        builder.Entity<Loan>()
            .HasOne(l => l.User)
            .WithMany(m => m.Loans)
            .HasForeignKey(l => l.UserId);
    }
}
