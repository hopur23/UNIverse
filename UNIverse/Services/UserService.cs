using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNIverse.Models;

namespace UNIverse.Services
{
    public class UserService
    {
        private ApplicationDbContext m_db;

        public UserService(ApplicationDbContext context)
        {
            m_db = context;
        }

        public ApplicationUser GetUserById(string id)
        {
            var user = (from u in m_db.Users
                        where u.Id == id
                        select u).SingleOrDefault();

            return user;
        }

        public void AddGroupToUser(Group group, ApplicationUser user)
        {
            user.Groups.Add(group);
            m_db.SaveChanges();
        }
    }
}