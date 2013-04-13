using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Chat.Hubs
{
  [HubName("chat")]
  public class ChatHub : Hub
  {
    public void Send(string message)
    {
      Clients.All.addMessage(message);
    }
  }
}