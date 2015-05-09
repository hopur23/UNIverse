using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNIverse.Models.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public University ParentUniversity { get; set; }
    }
}