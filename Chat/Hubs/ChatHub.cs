using Chat.BackendStorage;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Chat.Hubs
{
  [HubName("chat")]
  public class ChatHub : Hub
  {
    private readonly IChatRepository _chatRepository;

    public ChatHub(IChatRepository chatRepository)
    {
      _chatRepository = chatRepository;
    }

    public void Send(string message)
    {
      Clients.All.addMessage(message);
    }
  }
}