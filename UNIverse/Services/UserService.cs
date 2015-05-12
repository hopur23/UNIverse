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

        public List<ApplicationUser> GetAllUsers()
        {
            var user = (from u in m_db.Users
                        orderby u.FirstName ascending
                        select u).ToList();
            return user;
        }

        public List<ApplicationUser> GetAllUsers(string searchString)
        {
            var users = (from p in m_db.Users
                          where p.FirstName.Contains(searchString) || 
                          p.LastName.Contains(searchString)
                          orderby p.FirstName ascending
                          select p).ToList();

            return users;
        }

        public ApplicationUser GetUserById(string id)
        {
            var user = (from u in m_db.Users
                        where u.Id == id
                        select u).SingleOrDefault();

            return user;
        }

        public void EditUser(ApplicationUser user)
        {
            ApplicationUser upd = GetUserById(user.Id);
            if (upd != null)
            {
                upd.FirstName = user.FirstName;
                upd.LastName = user.LastName;
                upd.Description = user.Description;
                upd.ProfilePicturePath = user.ProfilePicturePath;
                m_db.SaveChanges();
            }
        }

        public void AddGroupToUser(Group group, ApplicationUser user)
        {
            user.Groups.Add(group);
            m_db.SaveChanges();
        }
    }
}