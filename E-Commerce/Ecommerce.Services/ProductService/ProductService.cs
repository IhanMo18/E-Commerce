using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;

namespace Ecommerce.Services.ProductService;

public class ProductService(IProductRepository repository) : Service<Product>(repository), IProductService
{

    public Product? GetProductsWithCategory(int productId)
    {
        return repository.GetProductsWithCategory(productId);
    }

    public Product? GetProductsWithAllReviews(int productId)
    {
        return repository.GetProductsWithAllReviews(productId);
    }
}