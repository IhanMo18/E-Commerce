using Ecommerce.Data.Data;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Repositories;

/// <summary>
/// Repository implementation for <see cref="Product"/>.
/// </summary>
public class ProductRepository(ApplicationDbContext dbContext) : Repository<Product>(dbContext), IProductRepository
{
    /// <inheritdoc />
    public async Task<Product?> GetProductsWithCategoryAsync(int productId)
    {
        var productWithCategory = await _dbContext.Products
            .Include(obj => obj.Category)
            .SingleOrDefaultAsync(product => product.Id == productId);

        if (productWithCategory == null)
        {
            Console.WriteLine($"Producto con ID {productId} no encontrado.");
        }

        return productWithCategory;
    }

    /// <inheritdoc />
    public async Task<Product?> GetProductsWithAllReviewsAsync(int productId)
    {
        return await _dbContext.Products
            .Include(obj => obj.Category)
            .Include(obj => obj.reviews)
            .SingleOrDefaultAsync(product => product.Id == productId);
    }
}
