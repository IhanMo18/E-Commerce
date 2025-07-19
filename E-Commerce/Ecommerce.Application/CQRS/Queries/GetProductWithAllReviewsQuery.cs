using Ecommerce.Application.CQRS.Mediator;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.CQRS.Queries;

public record GetProductWithAllReviewsQuery(int Id) : IRequest<Product?>;
