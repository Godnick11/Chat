using Chat.BackendStorage;
using Chat.Models;
using Chat.Utils;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Security.Application;
using System.Web;

namespace Chat.Hubs
{
  [HubName("chat")]
  public class ChatHub : Hub
  {
    private readonly IChatRepository _chatRepository;

    private string CurrentUserEmail
    {
      get
      {
        var result = HttpContext.Current.User.Identity.Name.ToLowerInvariant();
        return result;
      }
    }

    public ChatHub(IChatRepository chatRepository)
    {
      _chatRepository = chatRepository;
    }

    public void Send(string message)
    {
      var savedMessage = _chatRepository.SaveMessage(CurrentUserEmail, message);
      var savedMessageToSend = savedMessage.ToViewModel();
      SanitizeMessage(savedMessageToSend);
      Clients.All.addMessage(savedMessageToSend);
    }

    private void SanitizeMessage(ChatMessage message)
    {
      message.Text = Encoder.HtmlEncode(message.Text);
      message.WhoPosted = Encoder.HtmlEncode(message.WhoPosted);
    }
  }
}