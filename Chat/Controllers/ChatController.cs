using System.Web.Mvc;

namespace Chat.Controllers
{
  [Authorize]
  public class ChatController : Controller
  {
    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }
  }
}