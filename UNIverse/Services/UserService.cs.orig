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

<<<<<<< HEAD
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
=======
        public void AddGroupToUser(Group group, ApplicationUser user)
        {
            user.Groups.Add(group);
>>>>>>> c9318c804c34bde38de0c252e940d896c4bbd9f5
            m_db.SaveChanges();
        }
    }
}