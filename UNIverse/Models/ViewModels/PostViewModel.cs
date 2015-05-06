using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNIverse.Models.ViewModels
{
    public class PostViewModel
    {
        public string Content { get; set; }
        public ApplicationUser Owner { get; set; }
        public string ImagePath { get; set; }
        public List<Comment> Comments { get; set; }
    }
}