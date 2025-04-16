
using Ecommerce.Data.Data;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;

namespace Ecommerce.Repository.Repositories;

public class NotificationsRepository : Repository<Notifications>, INotificationsRepository
{
    
    public NotificationsRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}