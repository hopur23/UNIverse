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
        public string Content { get; set; }
<<<<<<< HEAD:UNIverse/Models/Entities/Comment.cs

=======
        // PostId
        // CommentId
>>>>>>> parent of f0298ad... Base application skeleton:UNIverse/Models/Comment.cs
    }
}