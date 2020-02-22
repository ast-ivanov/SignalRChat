using ChatApi.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(Message message)
        {
            Clients.All.SendAsync("received", message);
        }
    }
}