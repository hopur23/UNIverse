using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNIverse.Models
{
    public class University
    {
        [Key]
        public int UniversityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public List<Group> Groups { get; set; }
        public string EmailEnding { get; set; }
    }
}