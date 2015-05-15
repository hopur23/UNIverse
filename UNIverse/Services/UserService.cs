using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNIverse.Models;
using UNIverse.Models.Entities;

namespace UNIverse.Services
{
    public class UserService
    {
        private ApplicationDbContext m_db;

        public UserService(ApplicationDbContext context)
        {
            m_db = context;
        }

        /// <summary>
        /// Gets a list of all users in the system, ordered by first name.
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> GetAllUsers()
        {
            var user = (from u in m_db.Users
                        orderby u.FirstName ascending
                        select u).ToList();
            return user;
        }

        /// <summary>
        /// Gets a list of all users matching the specified search string.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<ApplicationUser> GetAllUsers(string searchString)
        {
            var users = (from p in m_db.Users
                          where (p.FirstName + " " + p.LastName).Contains(searchString)
                          orderby p.FirstName ascending
                          select p).ToList();

            return users;
        }

        /// <summary>
        /// Gets a specified user by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationUser GetUserById(string id)
        {
            var user = (from u in m_db.Users
                        where u.Id == id
                        select u).SingleOrDefault();

            return user;
        }

        /// <summary>
        /// Updates specified user in the system
        /// </summary>
        /// <param name="user"></param>
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

        /// <summary>
        /// Adds specified user to specified group
        /// </summary>
        /// <param name="group"></param>
        /// <param name="user"></param>
        public void AddGroupToUser(Group group, ApplicationUser user)
        {
            user.Groups.Add(group);
            m_db.SaveChanges();
        }

        /// <summary>
        /// Gets a single user by his email. If none is found, returns null.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApplicationUser GetUserByEmail(string email)
        {
            var user = (from u in m_db.Users
                        where u.Email == email
                        select u).SingleOrDefault();

            return user;
        }
    }
}