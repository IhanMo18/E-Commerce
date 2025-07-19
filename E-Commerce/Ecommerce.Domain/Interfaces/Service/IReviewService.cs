using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interface.Service;

public interface IReviewService : IService<Reviews>
{
    Task<Product?> SearchReviewByProducts(int productId);
}
