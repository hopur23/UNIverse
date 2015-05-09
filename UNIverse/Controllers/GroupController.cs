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
            var group = new Group();

            group.Id = group.Id;
            group.Name = viewModel.Name;
            group.Description = viewModel.Description;
            group.Members = new List<ApplicationUser>();
            group.Posts = new List<Post>();
            group.Administrator = ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId());

            ServiceWrapper.GroupService.AddGroup(group);

            // TODO: Ákveða hvert á að redirecta user
            return RedirectToAction("Index", "Home");
        }

    }
}