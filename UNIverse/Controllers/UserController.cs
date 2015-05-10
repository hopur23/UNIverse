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
        public ActionResult Index(string userId)
        {
            if(userId != null)
            {
                ApplicationUser user = new ApplicationUser();
                user = ServiceWrapper.UserService.GetUserById(userId);
                //user = ServiceWrapper.Services.UserService.GetUserById(userId);
                if(user == null)
                {
                    // TODO: Hafa flotta 404 síðu
                    return View("Error");
                }

                UserProfileViewModel viewModel = new UserProfileViewModel();

                viewModel.Name = user.FirstName + " " + user.LastName;
                viewModel.Email = user.Email;
                viewModel.Posts = user.Posts.OrderByDescending(p => p.DateCreated).ToList();
                viewModel.Groups = user.Groups.OrderByDescending(p => p.Name).ToList();
                return View(viewModel);
            }
            return View("Error");
            
        }
        public ActionResult Friends()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string userId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit()
        {
            return View();
        }

    }
}