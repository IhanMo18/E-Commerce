using Ecommerce.Application.CQRS.Mediator;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.CQRS.Queries;

/// <summary>
/// Handles requests for product with all reviews information.
/// </summary>
public class GetProductWithAllReviewsHandler(IProductRepository repository) : IRequestHandler<GetProductWithAllReviewsQuery, Product?>
{
    private readonly IProductRepository _repository = repository;

    /// <inheritdoc />
    public async Task<Product?> Handle(GetProductWithAllReviewsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetProductsWithAllReviewsAsync(request.Id);
    }
}
