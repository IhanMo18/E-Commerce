
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace Dashboard.Models.Hub;

public class MessageHub :Microsoft.AspNetCore.SignalR.Hub
{
    private static Dictionary<string, string> userConnections = new Dictionary<string, string>();
    private static HashSet<string> connectedClients = new HashSet<string>();
    
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
                break;
            case RoleType.Client:
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Client-{userId}");
                break;
        }
    }
    
    // Cliente envía mensaje al soporte
    public async Task SendToSupport(string userId, string message)
    {
        await Clients.Group(RoleType.Admin).SendAsync("ReceiveMessageFromUser", userId, message);
    }

    
    
    // Soporte responde a un cliente específico
    public async Task SendToUser(string userId, string message)
    {
        await Clients.Group($"Client-{userId}").SendAsync("ReceiveSupportMessage", message);
    }
    
    
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
                await Clients.Group(RoleType.Admin).SendAsync("UpdateClientList", connectedClients.ToList());
            }
        }

        await base.OnDisconnectedAsync(exception);
    }
}
