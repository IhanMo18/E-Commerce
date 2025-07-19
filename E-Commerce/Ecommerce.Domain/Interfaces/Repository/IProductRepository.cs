using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interface.Repository;

/// <summary>
/// Repository contract for product specific queries.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Retrieve a product with its category information.
    /// </summary>
    /// <param name="id">Identifier of the product.</param>
    /// <returns>Product instance or <c>null</c> when not found.</returns>
    Task<Product?> GetProductsWithCategoryAsync(int id);

    /// <summary>
    /// Retrieve a product with all its reviews.
    /// </summary>
    /// <param name="id">Identifier of the product.</param>
    /// <returns>Product instance or <c>null</c> when not found.</returns>
    Task<Product?> GetProductsWithAllReviewsAsync(int id);
}
