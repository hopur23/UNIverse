using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNIverse.Models;
using UNIverse.Services;

namespace UNIverse.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

       // [Authorize]
        public ActionResult Index()
        {
            // TODO: Nota ViewModel í staðinn (þarf mögulega að hafa með einhver ID? Til dæmis post ID eða user ID, til að hægt sé að opna userinn eða postinn)
            List<Post> model = ServiceWrapper.Instance.PostService.GetAllPosts();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}