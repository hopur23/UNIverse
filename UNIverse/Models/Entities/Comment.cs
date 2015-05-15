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
        public int Id { get; set; }
        public string Body { get; set; }
        public System.DateTime DateCreated { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual Post Parent { get; set; }
    }
}