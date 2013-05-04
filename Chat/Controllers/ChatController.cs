﻿using System.Web.Mvc;

namespace Chat.Controllers
{
  [Authorize]
  public class ChatController : BaseController
  {
    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }
  }
}