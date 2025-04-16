
namespace Ecommerce.Domain.Models;

public class Cart
{
    public int CartId { get; set; }
    public string SessionId { get; set; }
    public DateTime LastActionTime { get;set;} 
    public string UserId { get; set; }
    public User? User { get; set; }
   
    public ICollection<ProductCart> ProductCart { get; set; }
}