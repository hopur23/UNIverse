using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNIverse.Models;

namespace UNIverse.Services
{
    public class GroupService
    {
        private ApplicationDbContext m_db;

        public GroupService(ApplicationDbContext context)
        {
            m_db = context;
        }
    }
}