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
        public int PostId { get; set; }
        public ApplicationUser Owner { get; set; }
        public Group ParentGroup { get; set; }
        public string Tag { get; set; }
        public List<Comment> Comments { get; set; }
        public string content { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}