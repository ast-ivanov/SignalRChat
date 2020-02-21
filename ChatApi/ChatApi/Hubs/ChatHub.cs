using Microsoft.AspNetCore.SignalR;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.SendAsync("received", message);
        }
    }
}