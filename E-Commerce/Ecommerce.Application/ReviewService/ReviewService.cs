using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;

namespace Ecommerce.Services.ReviewService;

public class ReviewService(IReviewsRepository repository,
    IProductService productService) :Service<Reviews>(repository),IReviewService
{
    public Product? SearchReviewByProducts(int productId)
    {
      return productService.GetProductsWithAllReviews(productId);
    }
}