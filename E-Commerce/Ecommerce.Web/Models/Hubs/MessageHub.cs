
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace Dashboard.Models.Hub;

public class MessageHub(IMessageRepository _messageRepository) :Microsoft.AspNetCore.SignalR.Hub
{
    private static Dictionary<string, string> userConnections = new Dictionary<string, string>();
    private static HashSet<string> connectedClients = new HashSet<string>();
    private static HashSet<string> connectedAdmins = new HashSet<string>();
    
    // Cuando el usuario se conecta, registra su ID
    public async Task Register(string userId, string rol)
    {
        // Guarda la conexión con el userId
        if (!userConnections.ContainsKey(userId))
        {
            userConnections.Add(userId, Context.ConnectionId);
        }
        else
        {
            userConnections[userId] = Context.ConnectionId;
        }

        switch (rol)
        {
            case RoleType.Admin:
                await Groups.AddToGroupAsync(Context.ConnectionId, RoleType.Admin);
                connectedAdmins.Add(userId);
                await SendUsersOnline();
                break;
            
            
            case RoleType.Client:
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Client-{userId}");
                connectedClients.Add(userId);
                await SendUsersOnline();
                break;
        }
    }
    
    
    // Cliente envía mensaje al soporte
    public async Task SendToSupport(string userSenderId,string userReceptorId, string messageText)
    {
        var message = new Message()
        {
            SenderId = userSenderId,
            ReceptorId = userReceptorId,
            MessageText = messageText,
            DateTime = DateTime.UtcNow
        };
        _messageRepository.Update(message);
        await Clients.Group(RoleType.Admin).SendAsync("ReceiveMessageFromUser",userSenderId,userReceptorId,messageText);
    }

    
    
    // Soporte responde a un cliente específico
    public async Task SendToUser(string userSenderId,string userReceptorId, string messageText)
    {
        var message = new Message()
        {
            SenderId = userSenderId,
            ReceptorId = userReceptorId,
            MessageText = messageText,
            DateTime = DateTime.UtcNow
        };
        _messageRepository.Update(message);
        await Clients.Group($"Client-{userReceptorId}").SendAsync("ReceiveSupportMessage",userReceptorId,messageText);
    }


    private async Task SendUsersOnline()
    {
        await Clients.All.SendAsync("OnlineAdminList", connectedAdmins.ToList());
        await Clients.Group(RoleType.Admin).SendAsync("OnlineClientList", connectedClients.ToList());
    }


    //Para obtener el historial de Conversaciones entre dos usuarios (cliente o admin)
    public async Task GetHistory(string user1Id, string user2Id)
    {
        var messages = await _messageRepository.GetConversationAsync(user1Id, user2Id);
        //Caller permite que solo el cliente que inooca el metoodo es el que lo recibe
        await Clients.Caller.SendAsync("ReceivedHistory", messages);
    }
    
   
    
    
    
    
    
    
    
    //Se Descconecta un Usuario Se borra de la Lista
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

        if (!string.IsNullOrEmpty(userId))
        {
            userConnections.Remove(userId);
            
            // Si es un cliente, quitarlo de la lista y notificar
            if (connectedClients.Contains(userId))
            {
                connectedClients.Remove(userId);
                await SendUsersOnline();
            }

            // Si es un admin, quitarlo de la lista y notificar
            if (connectedAdmins.Contains(userId))
            {
                connectedAdmins.Remove(userId);
                await SendUsersOnline();
            }
        }
        await base.OnDisconnectedAsync(exception);
    }
}
