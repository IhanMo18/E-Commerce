using Ecommerce.Data.Data;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories;

public class MessageRepository(ApplicationDbContext dbContext) : Repository<Message>(dbContext), IMessageRepository
{
    public async Task<List<Message>> GetConversationAsync(string user1Id, string user2Id)
    {
       return await _dbContext.Set<Message>().Where(m => m.SenderId == user1Id && m.ReceptorId == user2Id
                                             || m.ReceptorId == user1Id && m.SenderId == user2Id)
            .OrderBy(m => m.DateTime).ToListAsync();
    }
}