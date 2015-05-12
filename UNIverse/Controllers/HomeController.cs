using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNIverse.Models;
using UNIverse.Models.ViewModels;
using UNIverse.Services;
using Microsoft.AspNet.Identity;

namespace UNIverse.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            // TODO: Sækja frekar latest posts frekar en alla. Implementa filters
            var model = ServiceWrapper.PostService.GetAllPostsFromFriends(this.User.Identity.GetUserId());

            return View(model);
        }

        public ActionResult Search(string searchString)
        {
            SearchViewModel model = new SearchViewModel();

            if (!String.IsNullOrEmpty(searchString))
            {
                model.Groups = ServiceWrapper.GroupService.GetAllGroups(searchString);
                model.Users = ServiceWrapper.UserService.GetAllUsers(searchString);
                return View(model);
            }

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

        public ActionResult AllUsers()
        {
            IEnumerable<ApplicationUser> model = ServiceWrapper.UserService.GetAllUsers();
            return View(model);
        }

    }
}