﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNIverse.Models.Entities;

namespace UNIverse.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The group must have a name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Image path")]
        public string GroupPicturePath { get; set; }

        public virtual University ParentUniversity { get; set; }
        public virtual ApplicationUser Administrator { get; set; }
        public virtual List<ApplicationUser> Members { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}