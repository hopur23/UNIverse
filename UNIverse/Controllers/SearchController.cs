using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNIverse.Models.ViewModels;
using Microsoft.AspNet.Identity;
using UNIverse.Models;
using UNIverse.Services;

namespace UNIverse.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string searchString)
        {
            var groups = ServiceWrapper.GroupService.GetAllGroups();
            var users = ServiceWrapper.UserService.GetAllUsers();

            if (!String.IsNullOrEmpty(searchString))
            {
           //     groups = groups.Where(s => s.Name.Contains(searchString));
            }

            return View(groups);
        }
    }
}