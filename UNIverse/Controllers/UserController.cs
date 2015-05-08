using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using UNIverse.Models;
using UNIverse.Models.ViewModels;

namespace UNIverse.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Friends()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}