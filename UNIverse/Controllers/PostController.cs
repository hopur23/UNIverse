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

        [HttpGet]
        public ActionResult AddComment(int? postId)
        {
            if(postId.HasValue)
            {
                var viewModel = new CommentViewModel()
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    ParentId = postId.Value,
                };

                return View(viewModel);
            }
            // TODO: Flottari error síða eða flottari villumeðhöndlun
            return View("Error");
        }

        [HttpPost]
        public ActionResult AddComment(CommentViewModel model)
        {
            if(ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    Body = model.Body,
                    Author = ServiceWrapper.UserService.GetUserById(model.AuthorId),
                    Parent = ServiceWrapper.PostService.GetPostById(model.ParentId),
                };

                ServiceWrapper.PostService.AddComment(comment);

                // TODO: Redirect til baka í póstinn sjálfann. Á eftir að útfæra single post view (þar sem hægt er að sjá post og öll komment sem fylgja).
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}