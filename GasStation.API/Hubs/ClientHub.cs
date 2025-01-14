using Microsoft.AspNetCore.SignalR;

namespace GasStation.API.Hubs
{
    public class ClientHub : Hub
    {
        private readonly ILogger<ClientHub> _logger;

        public ClientHub(ILogger<ClientHub> logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            var clientId = Context.ConnectionId;
            _logger.LogInformation($"Client connected: {clientId}");  // Логирование подключения
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var clientId = Context.ConnectionId;
            if (exception != null)
            {
                _logger.LogError(exception, $"Client disconnected with error: {clientId}");
            }
            else
            {
                _logger.LogInformation($"Client disconnected: {clientId}");
            }
            return base.OnDisconnectedAsync(exception);
        }

        public Task SendMessageToAll(string message)
        {
            _logger.LogInformation($"Broadcasting message: {message}");  // Логирование отправки сообщения
            return Clients.All.SendAsync("ReceiveMessage", message);
        }

        public Task Ping(string message)
        {
            _logger.LogInformation($"Received message from client: {message}");
            return Clients.All.SendAsync("Pong", $"Echo: {message}");
        }

    }
}
