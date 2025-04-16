using Ecommerce.Data.Data;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;

namespace Ecommerce.Repository.Repositories;

public class OrderDetailRepository :Repository<OrderDetail>,IOrderDetailRepository
{
    public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}