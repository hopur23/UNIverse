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

        public List<Group> GetAllGroups()
        {
            var groups = (from p in m_db.Groups
                         orderby p.Name descending
                         select p).ToList();

            return groups;
        }

        public Group GetGroupById(int id)
        {
            var group = (from p in m_db.Groups
                        where p.Id == id
                        select p).SingleOrDefault();

            return group;
        }

        public List<ApplicationUser> GetUsersByGroupId(int id)
        {
            var group = GetGroupById(id);
            return group.Members;
        }

        public void AddGroup(Group group)
        {
            m_db.Groups.Add(group);
            m_db.SaveChanges();
        }

        public void AddMemberToGroup(Group group, ApplicationUser user)
        {
            group.Members.Add(user);
            m_db.SaveChanges();
        }

    }
}