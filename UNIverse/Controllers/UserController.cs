using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using UNIverse.Models;
using UNIverse.Models.ViewModels;
using UNIverse.Services;
using UNIverse.Models.Entities;

namespace UNIverse.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index(string userId)
        {
            if(userId != null)
            {
                var user = ServiceWrapper.UserService.GetUserById(userId);
                //user = ServiceWrapper.Services.UserService.GetUserById(userId);
                if(user == null)
                {
                    // TODO: Hafa flotta 404 síðu
                    return View("Error");
                }

                var viewModel = new UserProfileViewModel();

                viewModel.Name = user.FirstName + " " + user.LastName;
                viewModel.Email = user.Email;
                viewModel.Posts = user.Posts.OrderByDescending(p => p.DateCreated).ToList();
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

        [HttpPost]
        public ActionResult AddFriend(string email)
        {
            if(email != null)
            {
                var receiver = ServiceWrapper.UserService.GetUserByEmail(email);
                var sender = ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId());

                if (receiver == null || sender == null)
                {
                    return View("Error");
                }
                
                var request = new FriendRequest()
                {
                    Sender = sender,
                    SenderId = sender.Id,
                    Receiver = receiver,
                    ReceiverId = receiver.Id,
                    RequestDate = DateTime.Now,
                    IsAccepted = false
                };

                ServiceWrapper.UserService.AddFriendRequest(request);

                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }

    }
}