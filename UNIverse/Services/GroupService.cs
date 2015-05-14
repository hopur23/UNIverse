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

        // Search for groups
        public List<Group> GetAllGroups(string searchString)
        {
            var groups = (from p in m_db.Groups
                            where p.Name.Contains(searchString)
                            orderby p.Name ascending
                            select p).ToList();
                
            return groups;
        }

        public List<Group> GetGroupsUserIsAdmin(string id)
        {
            var groups = (from p in m_db.Groups
                          where p.Administrator.Id == id
                          orderby p.Name ascending
                          select p).ToList();
            return groups;
        }

        public List<Group> GetGroupsUserIsIn(ApplicationUser user)
        {
            var groups = (from p in m_db.Groups
                          where (p.Administrator.Id != user.Id) &&
                          (p.Members.Contains(user))
                          orderby p.Name ascending
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

        public bool UserInGroup(int GroupId, string UserId)
        {
            List<ApplicationUser> allUsers = GetUsersByGroupId(GroupId);

            foreach (var item in allUsers)
	        {
		        if(item.Id == UserId)
                {
                    return true;
                }
	        }

            return false;
        }

        public void AddGroup(Group group)
        {
            m_db.Groups.Add(group);
            m_db.SaveChanges();
        }

        public void EditGroup(Group group)
        {
            Group g = ServiceWrapper.GroupService.GetGroupById(group.Id);
            if (g != null)
            {
                g.Name = group.Name;
                g.Description = group.Description;
                g.GroupPicturePath = group.GroupPicturePath;
                m_db.SaveChanges();
            }
        }

        public void AddMemberToGroup(Group group, ApplicationUser user)
        {
            group.Members.Add(user);
            m_db.SaveChanges();
        }

        public void RemoveMemberFromGroup(Group group, ApplicationUser user)
        {
            group.Members.Remove(user);
            m_db.SaveChanges();
        }

    }
}