using Ecommerce.Data.Data;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : Repository<Product>(dbContext), IProductRepository
{
    public Product? GetProductsWithCategory(int productId)
    {
        var productWithCategory = _dbContext.Products
            .Include(obj => obj.Category)
            .SingleOrDefault(product => product.Id == productId); 
        Console.WriteLine($"Producto con ID {productId} no encontrado.");
        return productWithCategory;
    }
    
    public Product? GetProductsWithAllReviews(int productId)
    {
        var productWithCategory = _dbContext.Products
            .Include(obj => obj.Category)
            .Include(obj=>obj.reviews)
            .SingleOrDefault(product => product.Id == productId);
        return productWithCategory;
    }
    
}