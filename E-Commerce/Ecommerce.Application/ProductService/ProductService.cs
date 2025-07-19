using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;

namespace Ecommerce.Services.ProductService;

/// <summary>
/// Service implementation for product operations.
/// </summary>
public class ProductService(IProductRepository repository) : Service<Product>(repository), IProductService
{
    /// <inheritdoc />
    public async Task<Product?> GetProductsWithCategoryAsync(int productId)
    {
        return await repository.GetProductsWithCategoryAsync(productId);
    }

    /// <inheritdoc />
    public async Task<Product?> GetProductsWithAllReviewsAsync(int productId)
    {
        return await repository.GetProductsWithAllReviewsAsync(productId);
    }
}
