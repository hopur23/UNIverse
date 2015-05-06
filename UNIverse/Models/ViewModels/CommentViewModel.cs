using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNIverse.Models.ViewModels
{
    public class CommentViewModel
    {
        public string Content { get; set; }
        public ApplicationUser Owner { get; set; }
    }
}