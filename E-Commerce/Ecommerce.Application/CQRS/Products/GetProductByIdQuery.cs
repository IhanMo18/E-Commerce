using Ecommerce.CQRS.Common;

namespace Ecommerce.Application.CQRS.Products;

public record GetProductByIdQuery(int Id) : IQuery<Domain.Models.Product?>;
