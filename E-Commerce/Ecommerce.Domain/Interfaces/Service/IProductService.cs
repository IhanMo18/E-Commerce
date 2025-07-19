using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interface.Service;

/// <summary>
/// Service contract for products.
/// </summary>
public interface IProductService : IService<Product>
{
    /// <summary>
    /// Retrieve a product with its category information.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    Task<Product?> GetProductsWithCategoryAsync(int id);

    /// <summary>
    /// Retrieve a product with all its reviews.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    Task<Product?> GetProductsWithAllReviewsAsync(int id);
}
