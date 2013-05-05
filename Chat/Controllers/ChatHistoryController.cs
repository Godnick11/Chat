using Chat.BackendStorage;
using Chat.Models;
using Chat.Utils;
using Microsoft.Security.Application;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Chat.Controllers
{
  [System.Web.Http.Authorize]
  public class ChatHistoryController : BaseApiController
  {
    private readonly IChatRepository _chatRepository;

    public ChatHistoryController()
    {
      // TODO: implement proper DI for WebApi controllers
      _chatRepository = System.Web.Mvc.DependencyResolver.Current.GetService<IChatRepository>();
    }

    [System.Web.Http.HttpGet]
    public IEnumerable<ChatMessage> GetLastMessages(int messageCount)
    {
      var result = _chatRepository.GetLastMessages(messageCount)
        .Select(msg => msg.ToViewModel())
        .ToArray();
      foreach (var msg in result)
      {
        SanitizeMessage(msg);
      }
      return result;
    }

    private void SanitizeMessage(ChatMessage message)
    {
      message.Text = Encoder.HtmlEncode(message.Text);
      message.WhoPosted = Encoder.HtmlEncode(message.WhoPosted);
    }
  }
}