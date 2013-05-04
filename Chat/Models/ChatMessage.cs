using System;

namespace Chat.Models
{
  public class ChatMessage
  {
    public string WhoPosted { set; get; }
    public DateTime WhenPosted { set; get; }
    public string Text { set; get; }
  }
}