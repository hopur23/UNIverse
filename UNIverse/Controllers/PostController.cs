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
        public ActionResult New(PostViewModel viewModel, string returnUrl)
        {
            Post post = new Post();

            if(viewModel.groupId.HasValue)
            {
                int realId = viewModel.groupId.Value;
                viewModel.ParentGroup = ServiceWrapper.GroupService.GetGroupById(realId);
            }
            post.Author = ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId());
            post.Body = viewModel.Body;
            post.Comments = new List<Comment>();
            post.DateCreated = DateTime.Now;
            post.ParentGroup = viewModel.ParentGroup;
            post.ImagePath = viewModel.ImagePath;
            post.Tag = viewModel.Tag;

            ServiceWrapper.PostService.AddPost(post);

            return Redirect(returnUrl);
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
        public ActionResult AddComment(CommentViewModel model, string returnUrl)
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
                return Redirect(returnUrl);

                //return RedirectToAction("Index", "Home");
            }
            return Redirect(returnUrl);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id.HasValue)
            {
                int realId = id.Value;
                var model = ServiceWrapper.PostService.GetPostById(realId);

                Post g = new Post();

                g.Id = model.Id;
                g.Author = model.Author;
                g.Body = model.Body;
                g.Comments = model.Comments;
                g.DateCreated = model.DateCreated;
                g.ParentGroup = model.ParentGroup;
                g.ImagePath = model.ImagePath;
                g.Tag = model.Tag;

                return View(g);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult Edit(Post n)
        {

            if (ModelState.IsValid)
            {
                Post post = new Post();
                post.Id = n.Id;
                post.Author = n.Author;
                post.Body = n.Body;
                post.Comments = n.Comments;
                post.DateCreated = n.DateCreated;
                post.ParentGroup = n.ParentGroup;
                post.ImagePath = n.ImagePath;
                post.Tag = n.Tag;
                ServiceWrapper.PostService.EditPost(post);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(n);
            }
        }

        public ActionResult DeletePost(int? id, string returnUrl)
        {
            if (id.HasValue)
            {
                int realId = id.Value;
                var post = ServiceWrapper.PostService.GetPostById(realId);

                ServiceWrapper.PostService.DeletePost(post);
            }

            return Redirect(returnUrl);
        }

        public ActionResult DeleteComment(int? id, string returnUrl)
        {
            if (id.HasValue)
            {
                int realId = id.Value;
                var comment = ServiceWrapper.PostService.GetCommentById(realId);

                if (comment.Author.Id == this.User.Identity.GetUserId())
                {
                    ServiceWrapper.PostService.DeleteComment(comment);
                }
            }

            return Redirect(returnUrl);
        }
    }
}