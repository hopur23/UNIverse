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
    public class PostController : Controller
    {
        [HttpGet]
        public ActionResult New()
        {
            PostViewModel viewModel = new PostViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult New(PostViewModel viewModel)
        {
            Post post = new Post();

            post.Author = ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId());
            //post.Author = ServiceWrapper.Services.UserService.GetUserById(this.User.Identity.GetUserId());
            post.Body = viewModel.Body;
            post.Comments = new List<Comment>();
            post.DateCreated = DateTime.Now;
            // TODO: Implement Groups
            //post.ParentGroup = viewModel.Group;
            // TODO: Implement Group Tags
            //post.Tag = viewModel.Tag;

            ServiceWrapper.PostService.AddPost(post);
            //ServiceWrapper.Services.PostService.AddPost(post);

            // TODO: Ákveða hvert á að redirecta user
            return RedirectToAction("Index", "Home");
        }
    }
}