using Ecommerce.Domain.Models;

namespace Dashboard.Models;

public class UsersVm
{
    
    public User User { get; set; }
    public IEnumerable<User> Users { get; set; }
    public IEnumerable<Product> Products { get; set; }
    
    public IEnumerable<Reviews> AllReviews { get; set; }
}