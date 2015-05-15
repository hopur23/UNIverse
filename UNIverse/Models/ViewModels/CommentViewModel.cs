using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNIverse.Models.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        [Display(Name = "Comment")]
        public string Body { get; set; }

        public string AuthorId { get; set; }
        public int ParentId { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}