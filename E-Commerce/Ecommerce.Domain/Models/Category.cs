using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Models;

public class Category
{
    [Key][Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public ICollection<Product> ProductsList { get; set; }
}