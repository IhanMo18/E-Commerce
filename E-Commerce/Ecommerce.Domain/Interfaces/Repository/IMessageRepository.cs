using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interface.Repository;

public interface IMessageRepository : IRepository<Message>
{
    Task<List<Message>> GetConversationAsync(string user1Id, string user2Id);
}