namespace Ecommerce.Domain.Models;

public class Message
{
    public int Id { get; set; }

    public string MessageText { get; set; }
    public string SenderId { get; set; }
    public User UserSender { get; set; }
    
    public string ReceptorId { get; set; } 
    public User UserReceptor { get; set; }
    public DateTime DateTime { get; set; }
}