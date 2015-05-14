using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNIverse.Models;
using UNIverse.Models.ViewModels;
using UNIverse.Services;
using Microsoft.AspNet.Identity;

namespace UNIverse.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private const int defaultEntryCount = 10;

        public ActionResult Index(int? postToID)
        {
            if (!postToID.HasValue)
                postToID = defaultEntryCount;

            if(Request.IsAjaxRequest())
            {                
                //Retrieve the page specified by the page variable with a page size o defaultEntryCount
                PostViewModel pagedEntries = GetModel(postToID.Value);        
        
                return PartialView("PostPage", pagedEntries);
            }

            //Retrieve the first page with a page size of entryCount
            PostViewModel entries = GetModel(0);
                
            AddMoreUrlToViewData();

            return View("Index", entries);
        }

     /*   {
            // TODO: Sækja frekar latest posts frekar en alla. Implementa filters
        //    var modeld = ServiceWrapper.PostService.GetAllPostsFromFriends(this.User.Identity.GetUserId());
            var model = GetModel(0);
            return View("Index", model);
        
           // return View(model);
        }*/

        private void AddMoreUrlToViewData()
        {
            ViewData["moreUrl"] = Url.Action("Index", "Home");
        }

        public ActionResult Search(string searchString)
        {
            SearchViewModel model = new SearchViewModel();

            if (!String.IsNullOrEmpty(searchString))
            {
                model.Groups = ServiceWrapper.GroupService.GetAllGroups(searchString);
                model.Users = ServiceWrapper.UserService.GetAllUsers(searchString);
                return View(model);
            }

            return View(model); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AllUsers()
        {
            IEnumerable<ApplicationUser> model = ServiceWrapper.UserService.GetAllUsers();
            return View(model);
        }


        protected PostViewModel GetModel(int toID = 0)
        {
            var posts = ServiceWrapper.PostService.GetAllPostsFromFriends(this.User.Identity.GetUserId());
            if (toID == 0)
            {
                return new PostViewModel
                {
                    Posts = posts.Take(defaultEntryCount).ToList()
                };
            }
            
            return new PostViewModel
            {
                Posts = posts.Where(p => p.Id < toID).Take(defaultEntryCount).OrderByDescending(m => m.Id).ToList()
            };
        }

        public ActionResult Tag(string tag)
        {
            PostViewModel viewModel = new PostViewModel();
            if (!String.IsNullOrEmpty(tag))
            {
                var posts = ServiceWrapper.PostService.PostsByTag(tag);
                viewModel = GetView(posts);
            }

            return View("Index", viewModel);
        }

       /* public ActionResult Groups()
        {
            var posts = ServiceWrapper.PostService.PostsByGroups(this.User.Identity.GetUserId());
            PostViewModel viewModel = GetView(posts);

            return View("Index", viewModel);
        }*/

        public PostViewModel GetView(List<Post> list)
        {
            if (list != null)
            {
                return new PostViewModel
                {
                    Posts = list.ToList()
                };
            }
            return new PostViewModel();
        }
    }
}