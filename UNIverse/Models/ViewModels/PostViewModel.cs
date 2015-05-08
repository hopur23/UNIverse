using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNIverse.Models.ViewModels
{
    public class PostViewModel
    {
        [Required]
        [Display(Name = "Post")]
        public string Body { get; set; }
        public ApplicationUser Author { get; set; }

        public List<CommentViewModel> Comments { get; set; }
        
    }
}