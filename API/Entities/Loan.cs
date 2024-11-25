﻿using System.Text.Json.Serialization;

namespace API.Entities;

public class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    [JsonIgnore]
    public Book Book { get; set; }
    public int UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}