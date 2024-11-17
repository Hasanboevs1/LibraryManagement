using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class RegisterModel
{
    [Required]
    public string Firstname { get; set; }
    [Required]
    public string Lastname { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfitrmPassword { get; set; }

    [Phone]
    [Required]
    public string Number { get; set; }
}
