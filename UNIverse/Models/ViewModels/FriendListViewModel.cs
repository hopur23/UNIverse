using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNIverse.Models.ViewModels
{
    public class FriendListViewModel
    {
        public ApplicationUser Owner { get; set; }
        public List<ApplicationUser> Friends { get; set; }
    }
}