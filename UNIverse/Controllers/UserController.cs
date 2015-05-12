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
            if (userId != null)
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
                    Posts = ServiceWrapper.PostService.GetAllPostsForProfileWall(user.Id),
                    Groups = user.Groups.OrderByDescending(g => g.Name).ToList(),
                    IsMyFriend = false
                    
                };
             if(ServiceWrapper.FriendService.GetFriendsForUser(this.User.Identity.GetUserId()).Contains(user) == true)
                    {
                        viewModel.IsMyFriend = true;
                    }

                viewModel.ProfilePicturePath = user.ProfilePicturePath;
                viewModel.Department = user.Department;
                viewModel.Description = user.Description;
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

        [HttpPost]
        public ActionResult AddFriend(string userId)
        {
            if (userId != null)
            {
                // User is trying to add himself as a friend. Redirect to his profile.
                if(userId == this.User.Identity.GetUserId())
                {
                    return RedirectToAction("Index", new { userId = userId });
                }
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