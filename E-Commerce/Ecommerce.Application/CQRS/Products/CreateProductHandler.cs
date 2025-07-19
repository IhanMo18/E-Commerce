using Ecommerce.CQRS.Common;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.CQRS.Products;

public class CreateProductHandler(IProductRepository repository) : IHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _repository = repository;

    public async Task<Product> HandleAsync(CreateProductCommand request)
    {
        var product = new Product { Name = request.Name, Details = request.Details, Price = request.Price };
        _repository.Add(product);
        _repository.Save();
        return product;
    }
}
