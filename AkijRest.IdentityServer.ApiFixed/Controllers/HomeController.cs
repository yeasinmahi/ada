﻿using System.Web.Mvc;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
