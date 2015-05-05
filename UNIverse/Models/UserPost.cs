using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNIverse.Models
{
    public class UserPost
    {
        public Guid ID { get; set; }
        public string content { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}