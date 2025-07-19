using Ecommerce.Application.CQRS.Mediator;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.CQRS.Queries;

public class GetProductWithAllReviewsHandler(IProductRepository repository) : IRequestHandler<GetProductWithAllReviewsQuery, Product?>
{
    private readonly IProductRepository _repository = repository;

    public Task<Product?> Handle(GetProductWithAllReviewsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetProductsWithAllReviews(request.Id));
    }
}
