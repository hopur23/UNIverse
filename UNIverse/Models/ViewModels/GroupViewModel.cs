using System;
using System.Collections.Generic;
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
    }
}