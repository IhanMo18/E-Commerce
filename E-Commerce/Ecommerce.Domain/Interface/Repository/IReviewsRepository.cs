using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interface.Repository;

public interface IReviewsRepository : IRepository<Reviews>
{
    public Product SearchReviewByProducts(int productId);
}