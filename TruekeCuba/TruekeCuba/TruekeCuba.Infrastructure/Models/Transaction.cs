namespace TruekeCuba.Infrastructure.Models;

public class Transaction
{
    Guid Id { get; set; }
    decimal Amount { get; set; }
    decimal TotalPrice {get;set;}
    
    
}