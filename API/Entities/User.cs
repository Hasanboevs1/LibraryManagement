using API.Enums;

namespace API.Entities;

public class User 
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Number { get; set; }
    public Role Role { get; set; } = Role.User;
    public ICollection<Loan> Loans { get; set; }
}
