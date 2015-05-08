using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNIverse.Models.ViewModels
{
    public class PostViewModel
    {
        public string Body { get; set; }
        public ApplicationUser Author { get; set; }

        public List<CommentViewModel> Comments { get; set; }
        
    }
}