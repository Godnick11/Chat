using Chat.BackendStorage;
using Chat.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace Chat.Controllers
{
  public class HomeController : Controller
  {
    private readonly IChatRepository _chatRepository;

    public HomeController(IChatRepository chatRepository)
    {
      _chatRepository = chatRepository;
    }

    [HttpGet]
    public ActionResult Index(LoginUserViewModel user)
    {
      return View(user);
    }

    [HttpPost]
    public ActionResult Login(LoginUserViewModel user)
    {
      if (!ModelState.IsValid)
        return RedirectToAction("Index", new { user = user });

      if (!_chatRepository.IsCredentialsValid(user.Email, user.Password))
      {
        ModelState.AddModelError(string.Empty, "email/password pair is not valid");
        return RedirectToAction("Index", new { user = user });
      }

      AuthorizeUser(user.Email, user.RememberMe);
      return RedirectToAction("Index", "Chat");
    }

    [HttpPost]
    public ActionResult Logout()
    {
      UnauthorizeCurrentUser();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Register(RegisterUserViewModel user)
    {
      return View(user);
    }

    [HttpPost]
    public ActionResult RegisterNewUser(RegisterUserViewModel user)
    {
      if (!ModelState.IsValid)
        return RedirectToAction("Register", new { user = user });
      if (!string.Equals(user.Password, user.RepeatPassword))
      {
        ModelState.AddModelError(string.Empty, "passwords don't match");
        return RedirectToAction("Register", new { user = user });
      }
      if (_chatRepository.IsUserRegistered(user.Email))
      {
        ModelState.AddModelError("Email", "the email is not available");
        return RedirectToAction("Register", new { user = user });
      }
      _chatRepository.AddNewUser(user);
      AuthorizeUser(user.Email, user.RememberMe);
      return RedirectToAction("Index", "Chat");
    }

    [HttpGet]
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public JsonResult IsEmailAvailable(string email)
    {
      var result = !_chatRepository.IsUserRegistered(email);
      return Json(result, JsonRequestBehavior.AllowGet);
    }

    private void AuthorizeUser(string email, bool rememberMe)
    {
      FormsAuthentication.SetAuthCookie(email, rememberMe);
    }

    private void UnauthorizeCurrentUser()
    {
      FormsAuthentication.SignOut();
      Session.Abandon();
      // clear authentication cookie
      var cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty);
      cookie1.Expires = DateTime.Now.AddYears(-1);
      Response.Cookies.Add(cookie1);
      // clear session cookie
      var cookie2 = new HttpCookie("ASP.NET_SessionId", string.Empty);
      cookie2.Expires = DateTime.Now.AddYears(-1);
      Response.Cookies.Add(cookie2);
    }
  }
}