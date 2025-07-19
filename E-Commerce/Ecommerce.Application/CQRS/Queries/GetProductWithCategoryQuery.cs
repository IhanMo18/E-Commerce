using Ecommerce.Application.CQRS.Mediator;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.CQRS.Queries;

public record GetProductWithCategoryQuery(int Id) : IRequest<Product?>;
