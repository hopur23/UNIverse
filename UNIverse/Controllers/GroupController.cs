using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UNIverse.Models.ViewModels;
using Microsoft.AspNet.Identity;
using UNIverse.Models;
using UNIverse.Services;

namespace UNIverse.Controllers
{
    public class GroupController : Controller
    {
        public ActionResult Index()
        {
            List<Group> model = ServiceWrapper.GroupService.GetAllGroups();
            return View(model); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            GroupViewModel viewModel = new GroupViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(GroupViewModel viewModel)
        {
            Group group = new Group();

        /*    public string Name { get; set; }
        public string Description { get; set; }
        public string GroupPicturePath { get; set; }
        public List<Post> Posts { get; set; }
        public List<ApplicationUser> Members { get; set; }*/

            group.Id = group.Id;
            group.Name = viewModel.Name;
            group.Description = viewModel.Description;
            group.Members = new List<ApplicationUser>();
        //    group.Posts = new List<Post>();

            ServiceWrapper.GroupService.AddGroup(group);
            //ServiceWrapper.Services.PostService.AddPost(post);

            // TODO: Ákveða hvert á að redirecta user
            return RedirectToAction("Index", "Home");
        }

    }
}