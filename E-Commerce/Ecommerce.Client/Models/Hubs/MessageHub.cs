
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace Dashboard.Models.Hub;

public class MessageHub(IMessageRepository _messageRepository) :Microsoft.AspNetCore.SignalR.Hub
{
    private static readonly Dictionary<string, string> userConnections = new();
    private static readonly HashSet<string> connectedClients = new();
    private static readonly HashSet<string> connectedAdmins = new();
    private static readonly HashSet<string> subscribedAdmins = new();
    private static readonly Dictionary<string, string> activeSessions = new();
    
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
                subscribedAdmins.Add(userId);
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
    public async Task SendToSupport(string userId, string messageText)
    {
        var message = new Message
        {
            SenderId = userId,
            ReceptorId = activeSessions.ContainsKey(userId) ? activeSessions[userId] : null,
            MessageText = messageText,
            DateTime = DateTime.UtcNow
        };
        _messageRepository.Update(message);

        if (activeSessions.TryGetValue(userId, out var adminId))
        {
            if (userConnections.TryGetValue(adminId, out var connId))
            {
                await Clients.Client(connId).SendAsync("ReceiveMessageFromUser", userId, messageText);
            }
        }
        else
        {
            foreach (var admin in subscribedAdmins)
            {
                if (userConnections.TryGetValue(admin, out var connId))
                {
                    await Clients.Client(connId).SendAsync("ReceiveMessageFromUser", userId, messageText);
                }
            }
        }
    }

    
    
    // Soporte responde a un cliente específico
    public async Task SendToUser(string adminId, string userId, string messageText)
    {
        if (!activeSessions.ContainsKey(userId))
        {
            activeSessions[userId] = adminId;
            await Clients.Group(RoleType.Admin).SendAsync("UserAssigned", userId, adminId);
        }
        else if (activeSessions[userId] != adminId)
        {
            return;
        }

        var message = new Message
        {
            SenderId = adminId,
            ReceptorId = userId,
            MessageText = messageText,
            DateTime = DateTime.UtcNow
        };
        _messageRepository.Update(message);
        await Clients.Group($"Client-{userId}").SendAsync("ReceiveSupportMessage", userId, messageText);
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

    public Task SubscribeSupport(string adminId)
    {
        subscribedAdmins.Add(adminId);
        return Task.CompletedTask;
    }

    public Task EndConversation(string adminId, string userId)
    {
        if (activeSessions.TryGetValue(userId, out var assigned) && assigned == adminId)
        {
            activeSessions.Remove(userId);
        }
        return Task.CompletedTask;
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
                subscribedAdmins.Remove(userId);

                var toRemove = activeSessions.Where(kvp => kvp.Value == userId).Select(kvp => kvp.Key).ToList();
                foreach (var key in toRemove)
                {
                    activeSessions.Remove(key);
                }

                await SendUsersOnline();
            }
        }
        await base.OnDisconnectedAsync(exception);
    }
}
