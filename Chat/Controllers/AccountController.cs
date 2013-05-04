using System.Web.Mvc;

namespace Chat.Controllers
{
  public class AccountController : BaseController
  {
    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }
  }
}