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
                // Hér er userinn aðeins kominn með þær friend requests sem hann sjálfur hefur sent.

                if(user == null)
                {
                    // TODO: Hafa flotta 404 síðu
                    return View("Error");
                }

                var viewModel = new UserProfileViewModel()
                {
                    userId = user.Id,
                    Name = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    ReceivedFriendRequests = ServiceWrapper.FriendService.GetReceivedFriendRequests(user.Id),
                    SentFriendRequests = ServiceWrapper.FriendService.GetSentFriendRequests(user.Id),
                    Friends = ServiceWrapper.FriendService.GetFriendsForUser(user.Id),
                    Posts = user.Posts.OrderByDescending(p => p.DateCreated).ToList(),
                    Groups = user.Groups.OrderByDescending(g => g.Name).ToList()
                };

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
        public ActionResult AddFriend(string userId)
        {
            if (userId != null)
            {
                var receiver = ServiceWrapper.UserService.GetUserById(userId);
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

                ServiceWrapper.FriendService.AddFriendRequest(request);

                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }
    }
}