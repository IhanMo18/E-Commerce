using Ecommerce.CQRS.Common;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.CQRS.Products;

public record CreateProductCommand(string Name, string Details, double Price) : ICommand<Product>;
