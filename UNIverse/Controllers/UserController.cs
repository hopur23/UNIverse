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
                viewModel.ProfilePicturePath = user.ProfilePicturePath;
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
            if (!String.IsNullOrEmpty(userId))
            {
                var model = ServiceWrapper.UserService.GetUserById(userId);

                ApplicationUser g = new ApplicationUser();

                g.Id = model.Id;
                g.FirstName = model.FirstName;
                g.LastName = model.LastName;
                g.Description = model.Description;
                g.Posts = model.Posts;
                g.ProfilePicturePath = model.ProfilePicturePath;
                g.Email = model.Email;
                g.UserName = model.UserName;

                return View(g);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult Edit(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Id = model.Id;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Description = model.Description;
                user.Posts = model.Posts;
                user.ProfilePicturePath = model.ProfilePicturePath;
                user.Email = model.Email;
                user.UserName = model.UserName;
                ServiceWrapper.UserService.EditUser(user);
                return RedirectToAction("Index", "User", new { userId = User.Identity.GetUserId() });
            }
            else
            {
                return View(model);
            }
        }

    }
}