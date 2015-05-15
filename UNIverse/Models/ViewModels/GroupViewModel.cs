using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNIverse.Models.ViewModels
{
    public class GroupViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string GroupPicturePath { get; set; }

        public List<Post> Posts { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public ApplicationUser Administrator { get; set; }

        public bool isAdmin { get; set; }
        public bool inGroup { get; set; }
        public bool search { get; set; }
        public int Id { get; set; }
        public List<Group> AllGroups { get; set; }
        public List<Group> UserIsAdmin { get; set; }
        public List<Group> GroupsUserIsIn { get; set; }
    }
}