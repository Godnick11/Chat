using Chat.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Chat.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet]
    public ActionResult Index(LoginUserViewModel user)
    {
      return View(user);
    }

    [HttpPost]
    public ActionResult Login(LoginUserViewModel user)
    {
      if (!ModelState.IsValid)
        RedirectToAction("Index", new { user = user });

      if (!IsCredentialsValid(user.Email, user.Password))
      {
        ModelState.AddModelError(string.Empty, "email/password pair is not valid");
        return RedirectToAction("Index", new { user = user });
      }

      return RedirectToAction("Index", "Chat");
    }

    [HttpPost]
    public ActionResult Logout()
    {
      UnauthorizeCurrentUser();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Register(RegisterUserViewModel user)
    {
      if (!ModelState.IsValid)
        RedirectToAction("Index", new { user = user });

      // TODO: save user
      AuthorizeUser(user.Email, user.RememberMe);
      return RedirectToAction("Index", "Chat");
    }

    private bool IsCredentialsValid(string email, string password)
    {
      // TODO: check credentials
      if (false)
      {
        return false;
      }
      return true;
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