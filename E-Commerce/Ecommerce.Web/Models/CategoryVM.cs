using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Dashboard.Models;

public class CategoryVM
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    
    
    [ValidateNever]
    public List<Product> ProductsList { get; set; }
}