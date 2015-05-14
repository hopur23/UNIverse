using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UNIverse.Models.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Silly billy! You can't post an empty status")]
        [Display(Name = "Post")]
        public string Body { get; set; }

        public ApplicationUser Author { get; set; }
        public List<Comment> Comments { get; set; }
        public Group ParentGroup { get; set; }
        public string Tag { get; set; }

        public int? groupId { get; set; }
        public string ImagePath { get; set; }
        public List<SelectListItem> Tags { get; set; }
        public List<Post> Posts { get; set; }
        public int currentPageNumber { get; set; }
    }
}