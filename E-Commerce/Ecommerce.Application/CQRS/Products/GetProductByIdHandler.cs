using Ecommerce.CQRS.Common;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.CQRS.Products;

public class GetProductByIdHandler(IProductRepository repository) : IHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository _repository = repository;

    public async Task<Product?> HandleAsync(GetProductByIdQuery request)
    {
        return await _repository.GetAsync(request.Id);
    }
}
