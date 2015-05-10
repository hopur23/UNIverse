using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using UNIverse.Models.Entities;

namespace UNIverse.Models.ViewModels
{
    public class SearchViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<Group> Groups { get; set; } 
    }
}