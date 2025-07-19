using Ecommerce.Application.CQRS.Mediator;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.CQRS.Queries;

/// <summary>
/// Handles requests for product with category information.
/// </summary>
public class GetProductWithCategoryHandler(IProductRepository repository) : IRequestHandler<GetProductWithCategoryQuery, Product?>
{
    private readonly IProductRepository _repository = repository;

    /// <inheritdoc />
    public async Task<Product?> Handle(GetProductWithCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetProductsWithCategoryAsync(request.Id);
    }
}
