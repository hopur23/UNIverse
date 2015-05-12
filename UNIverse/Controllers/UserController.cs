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
                // Fetch the user and yourself from the database.
                var receiver = ServiceWrapper.UserService.GetUserById(userId);
                var sender = ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId());

                // Either the user does not exist or you are not authenticated. Return an error view.
                if (receiver == null || sender == null)
                {
                    return View("Error");
                }

                // Check if there is already a friend request between the users in the system
                var req = ServiceWrapper.FriendService.FindRequestBetween(sender.Id, receiver.Id);

                // Already exists a request
                if(req != null)
                {
                    // Is the other user the receiver?
                    // Nothing happens, you can't send two requests and you can't accept it on behalf of the other user.
                    // Redirect the user to the other user's profile.
                    // This should never happen under regular circumstances, the option to add a friend on an already
                    // added friend should not be available.
                    if(req.Receiver.Id == userId)
                    {
                        return RedirectToAction("Index", new { userId = userId });

                    }
                    // If you got here, you are the receiver.
                    // Is the request pending? Then accept it.
                    if(req.IsAccepted == false)
                    {
                        req.IsAccepted = true;
                        ServiceWrapper.FriendService.UpdateFriendRequest(req);
                    }
                }
                // No previous friend request was found. Create a new pending one.
                else
                {
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
                }
                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }
    }
}