using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Models;

public class Notifications
{
    [Key]
    public int NotificationsId  { get; set; }
    public String Message { get; set; }
    public NotificationType Type { get; set; }
    public DateTime Date { get; set; }
    public bool Read { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }

}