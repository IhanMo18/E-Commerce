using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;

namespace Ecommerce.Services.ReviewService;

/// <summary>
/// Service for handling reviews.
/// </summary>
public class ReviewService(IReviewsRepository repository,
    IProductService productService) :Service<Reviews>(repository),IReviewService
{
    /// <summary>
    /// Obtain a product with its reviews.
    /// </summary>
    public async Task<Product?> SearchReviewByProducts(int productId)
    {
      return await productService.GetProductsWithAllReviewsAsync(productId);
    }
}
