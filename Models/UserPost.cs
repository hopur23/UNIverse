using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNIverse.Models
{
    public class UserPost
    {
        [Key]
        public int PostId { get; set; }
        public string content { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}