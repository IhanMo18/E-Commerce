
using Ecommerce.Data.Data;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Repositories;

namespace Dashboard.Repository;

public class OrderRepository : BaseRepository<Order>,IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}