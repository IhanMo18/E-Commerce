using Ecommerce.Application.CQRS.Mediator;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.CQRS.Queries;

public class GetProductWithCategoryHandler(IProductRepository repository) : IRequestHandler<GetProductWithCategoryQuery, Product?>
{
    private readonly IProductRepository _repository = repository;

    public Task<Product?> Handle(GetProductWithCategoryQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetProductsWithCategory(request.Id));
    }
}
