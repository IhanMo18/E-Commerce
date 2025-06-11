using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interface.Service;

public interface IReviewService : IService<Reviews>
{
    public Product? SearchReviewByProducts(int productId);
}