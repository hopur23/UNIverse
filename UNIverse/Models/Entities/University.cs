using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNIverse.Models.Entities;


namespace UNIverse.Models
{
    public class University
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmailEnding { get; set; }

        public virtual List<ApplicationUser> Members { get; set; }
        public virtual List<Group> Groups { get; set; }
    }
}