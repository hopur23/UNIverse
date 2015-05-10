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

        public ApplicationUser GetUserById(string id)
        {
            var user = (from u in m_db.Users
                        where u.Id == id
                        select u).SingleOrDefault();

            return user;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            var users = (from u in m_db.Users
                         select u).ToList();

            return users;
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            var user = (from u in m_db.Users
                        where u.Email == email
                        select u).SingleOrDefault();

            return user;
        }

        public void AddFriendRequest(FriendRequest request)
        {
            m_db.FriendRequests.Add(request);
            m_db.SaveChanges();
        }
    }
}