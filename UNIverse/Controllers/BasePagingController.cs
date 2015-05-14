using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNIverse.Services;
using UNIverse.Models;
using UNIverse.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace UNIverse.Controllers
{
    public class BasePagingController : Controller
    {
        public ActionResult PostsPartial()
        {
            var model = GetModel(0);
            return PartialView("PostsPartial", model);
        }

        public JsonResult CheckPostsStatus(int currentPageNumber)
        {
            var posts = ServiceWrapper.PostService.GetAllPostsFromFriends(this.User.Identity.GetUserId());
            var totalPages = (int)Math.Ceiling((decimal)posts.Count() / 15);
            return Json((currentPageNumber + 1) < totalPages, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadMorePosts(int currentPageNumber)
        {
            var model = GetModel(currentPageNumber);
            return PartialView("PostDisplayer", model);
        }

        protected PostViewModel GetModel(int currentPageNumber)
        {
            var posts = ServiceWrapper.PostService.GetAllPostsFromFriends(this.User.Identity.GetUserId());
            if (currentPageNumber == 0)
            {
                return new PostViewModel
                {
                    Posts = posts.Take(NumberOfPosts).ToList()
                };
            }

            var currentPage = currentPageNumber + 1;
            return new PostViewModel
            {
                Posts = posts.Skip((currentPage - 1) * NumberOfPosts).Take(NumberOfPosts).ToList()
            };
        }

        private const int NumberOfPosts = 15;
    }
}