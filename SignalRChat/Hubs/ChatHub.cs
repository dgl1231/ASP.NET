using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
    }
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(string user, string message)
        {
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.All.ReceiveMessage(user, message);
        }
        public Task SendMessageToCaller(string user, string message)
        {
            //await Clients.Caller.SendAsync("ReceiveMessage", user, message);
            return Clients.Caller.ReceiveMessage(user, message);

        }
        public async Task SendMessageToGroup(string user, string message)
        {
            //await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", user, message);
            await Clients.Group("SignalR Users").ReceiveMessage(user, message);

        }
        [HubMethodName("SendMessageToUser")]
        public Task DirectMessage(string user, string message)
        {
            return Clients.User(user).ReceiveMessage(user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Group("SignalR Users").ReceiveMessage("I", "disconnect");
            await base.OnDisconnectedAsync(exception);
        }
    }
}