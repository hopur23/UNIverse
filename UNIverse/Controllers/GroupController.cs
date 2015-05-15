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
        private const int defaultEntryCount = 8;

        public ActionResult Index()
        {
            var user = ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId());
            var model = new GroupViewModel();

            model.AllGroups = ServiceWrapper.GroupService.GetAllGroups();
            if (user.Groups != null)
            {
                model.GroupsUserIsIn = ServiceWrapper.GroupService.GetGroupsUserIsIn(user);
                model.UserIsAdmin = ServiceWrapper.GroupService.GetGroupsUserIsAdmin(this.User.Identity.GetUserId());
            }
            model.search = false;

            return View(model); 
        }

        public ActionResult SeeAllGroups(string searchString)
        {
            var groups = new GroupViewModel();
            groups.search = false;

            if (!String.IsNullOrEmpty(searchString))
            {
                groups.search = true;
                groups.AllGroups = ServiceWrapper.GroupService.GetAllGroups(searchString);
                return View("SeeAll", groups);
            }

            groups.AllGroups = ServiceWrapper.GroupService.GetAllGroups();

            return View("SeeAll", groups);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                int realId = id.Value;
                var model = ServiceWrapper.GroupService.GetGroupById(realId);

                Group g = new Group();

                g.Id = model.Id;
                g.Name = model.Name;
                g.Description = model.Description;
                g.Administrator = model.Administrator;
                g.Members = model.Members;
                g.Posts = model.Posts;
                g.GroupPicturePath = model.GroupPicturePath;

                return View(g);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult Edit(Group n)
        {
            if (ModelState.IsValid)
            {
                Group group = new Group();
                group.Id = n.Id;
                group.Name = n.Name;
                group.Description = n.Description;
                group.GroupPicturePath = n.GroupPicturePath;
                group.Administrator = n.Administrator;
                group.Members = n.Members;
                group.Posts = n.Posts;
                ServiceWrapper.GroupService.EditGroup(group);
                return RedirectToAction("View", "Group", new { id = group.Id });
            }
            else
            {
                return View(n);
            }
        }

        public ActionResult View(int id)
        {
            Group group = ServiceWrapper.GroupService.GetGroupById(id);
            if (group != null)
            {
                GroupViewModel viewModel = new GroupViewModel();
                viewModel.isAdmin = false;
                viewModel.inGroup = false;

                if (ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId()) == group.Administrator)
                {
                    viewModel.isAdmin = true;
                }

                if (ServiceWrapper.GroupService.UserInGroup(group.Id, this.User.Identity.GetUserId()))
                {
                    viewModel.inGroup = true;
                }

                viewModel.Name = group.Name;
                viewModel.Description = group.Description;
                viewModel.Members = group.Members;
                viewModel.Posts = group.Posts.OrderByDescending(m => m.DateCreated).Take(defaultEntryCount).ToList();
                viewModel.Id = group.Id;
                viewModel.GroupPicturePath = group.GroupPicturePath;
                viewModel.Administrator = group.Administrator;

                ViewData["moreUrl"] = Url.Action("GetGroupPosts", "Group");

                return View(viewModel);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult GetGroupPosts(int postToID, int groupId)
        {
            Group group = ServiceWrapper.GroupService.GetGroupById(groupId);
            var posts = group.Posts;
            //Retrieve the page specified by the page variable with a page size o defaultEntryCount

            PostViewModel pagedEntries = new PostViewModel
            {
                Posts = posts.Where(p => p.Id < postToID).OrderByDescending(m => m.Id).Take(defaultEntryCount).ToList()
            };

            return PartialView("PostPage", pagedEntries);
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
            group.GroupPicturePath = viewModel.GroupPicturePath;
           
            group.Members.Add(ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId()));

            ServiceWrapper.GroupService.AddGroup(group);

            // Add the group to the group list of User
           // ServiceWrapper.UserService.AddGroupToUser(group, ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId()));

            // TODO: Ákveða hvert á að redirecta user
            return RedirectToAction("View", "Group", new { id = group.Id });
        }


        [HttpGet]
        public ActionResult Join(int id)
        {
            var group = ServiceWrapper.GroupService.GetGroupById(id);
            var user = ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId());

            ServiceWrapper.GroupService.AddMemberToGroup(group, user);
          //  ServiceWrapper.UserService.AddGroupToUser(group, user);

            // TODO: Ákveða hvert á að redirecta user
            return RedirectToAction("View", "Group", new { id = group.Id });
        }

        [HttpGet]
        public ActionResult Leave(int id)
        {
            var group = ServiceWrapper.GroupService.GetGroupById(id);
            var user = ServiceWrapper.UserService.GetUserById(this.User.Identity.GetUserId());

            if (user == group.Administrator)
            {
                return RedirectToAction("Index", "Group");
            }

            ServiceWrapper.GroupService.RemoveMemberFromGroup(group, user);
            //  ServiceWrapper.UserService.AddGroupToUser(group, user);

            // TODO: Ákveða hvert á að redirecta user
            return RedirectToAction("Index", "Group");
        }

        

    }
}