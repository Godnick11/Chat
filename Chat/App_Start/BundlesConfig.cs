﻿using System.Web.Optimization;

namespace Chat
{
  public static class BundlesConfig
  {
    public static void RegisterBundles(BundleCollection bundles)
    {
      var jquery = new ScriptBundle("~/bundles/jquery")
          .Include("~/Scripts/jquery-{version}.js",
                  "~/Scripts/jquery-ui-{version}.js");

      var bootstrapjs = new ScriptBundle("~/bundles/bootstrapjs")
          .Include("~/Scripts/bootstrap.js");

      var signalr = new ScriptBundle("~/bundles/signalr")
          .Include("~/Scripts/jquery.signalR-1.0.1.js");

      var chat = new ScriptBundle("~/bundles/chat")
          .Include("~/Scripts/chat.js");

      var jQueryUnobtrusiveValidation = new ScriptBundle("~/bundles/jQuery-unobtrusive-validation")
          .Include("~/Scripts/jquery.validate.js",
                  "~/Scripts/jquery.validate.unobtrusive.js");

      var account = new ScriptBundle("~/bundles/account")
          .Include("~/Scripts/account.js");

      bundles.Add(jquery);
      bundles.Add(bootstrapjs);
      bundles.Add(signalr);
      bundles.Add(chat);
      bundles.Add(jQueryUnobtrusiveValidation);
      bundles.Add(account);
    }
  }
}