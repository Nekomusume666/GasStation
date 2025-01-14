using Microsoft.AspNetCore.SignalR;

namespace GasStation.API.Hubs
{
    public class NotificationHub : Hub
    {
        // Метод для отправки сообщения всем подключенным клиентам
        public async Task BroadcastMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Метод для отправки сообщения конкретной группе
        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }

        // Добавление клиента в группу
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        // Удаление клиента из группы
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
