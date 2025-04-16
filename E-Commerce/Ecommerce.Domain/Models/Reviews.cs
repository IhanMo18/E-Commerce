using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Models;

public class Reviews
{
    [Key]
    public int IdReview { get; set; }
    public DateTime Date { get; set; }
    public double Stars { get; set; }
    
    public string Message { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}