using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interface.Service;

public interface IProductService : IService<Product>
{
    public Product? GetProductsWithCategory(int id);
    public Product? GetProductsWithAllReviews(int id);
}