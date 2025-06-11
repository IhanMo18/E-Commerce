using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dashboard.Models;

public class ProductVm
{

    [ValidateNever]
    public Product Product { get; set; }
    
    [ValidateNever]
    public IEnumerable<SelectListItem> Categories { get; set; }
    
    
    [ValidateNever]
    public Category Category { get; set; }
    
    [ValidateNever]
    
    public ICollection<Reviews>? AllReviews { get; set; }
    
    [ValidateNever]
    public Reviews Review { get; set; }
    
}