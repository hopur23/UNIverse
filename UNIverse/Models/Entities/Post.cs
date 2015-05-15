using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNIverse.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Post cannot be empty")]
        public string Body { get; set; }
        public string Tag { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string ImagePath { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual Group ParentGroup { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}