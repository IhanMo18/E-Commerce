using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;

namespace Ecommerce.Services.MessageService;

public class MessageService(IMessageRepository repository) : Service<Message>(repository),IMessageService
{
    
}