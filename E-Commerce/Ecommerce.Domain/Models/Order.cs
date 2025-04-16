using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Models;

public class Order
{
    [Key]
    public int IdOrder{ get; set; }
    public String Gui { get; set; }
    public string UserId { get; set; }
    public User User{ get; set; }
    public int OrderDetailId { get; set; }
    public ICollection<OrderDetail> OrderDetail { get; set; }
    public OrderStatus Status { get; set; }
}