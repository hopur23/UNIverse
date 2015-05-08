using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using UNIverse.Models;
using UNIverse.Models.ViewModels;
using UNIverse.Services;

namespace UNIverse.Controllers
{
    public class UserController : Controller
    {

       // public ActionResult Index()
        //{
        //}
        public ActionResult Friends()
        {
            return View();
        }

        public ActionResult Profile(string userId)
        {
            ApplicationUser user = new ApplicationUser();
            user = ServiceWrapper.Instance.UserService.GetUserById(userId);

            UserProfileViewModel viewModel = new UserProfileViewModel();

            viewModel.Name = user.FirstName + " " + user.LastName;
            return View(viewModel);
        }

        public ActionResult Edit()
        {
            return View();
        }


    }
}