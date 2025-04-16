using Ecommerce.Data.Data;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;

namespace Ecommerce.Repository.Repositories;

public class ReviewsRepository (ApplicationDbContext dbContext) : Repository<Reviews>(dbContext),IReviewsRepository
{
    
    public Product SearchReviewByProducts(int productId)
    {
        throw new NotImplementedException();
    }
}