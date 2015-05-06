using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNIverse.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public ApplicationUser Owner { get; set; }
        public Post Parent { get; set; }
        public string Content { get; set; }
    }
}