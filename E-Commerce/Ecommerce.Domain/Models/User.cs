using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Models;

public class User : IdentityUser
{
    
    [Required][EmailAddress][Display(Name = "Email Address")]
    public override string Email { get; set; }  
    
    public bool AccountStatus{ get; set; }
    public string SessionId { get; set; }
    
    
    
    
    public String ImgProfile{ get; set; }

    public ICollection<Order> Order{ get; set; }
    public ICollection<Notifications> Notifications{ get; set; }
    
    public ICollection<Reviews> Reviews{ get; set; }
    
    public Cart Cart { get; set; }
    
    
}