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

        /// <summary>
        /// Get a list of all the groups in the system.
        /// </summary>
        /// <returns></returns>
        public List<Group> GetAllGroups()
        {
            var groups = (from p in m_db.Groups
                          orderby p.Name descending
                          select p).ToList();

            return groups;
        }

        /// <summary>
        /// Search for groups with given search string.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Group> GetAllGroups(string searchString)
        {
            var groups = (from p in m_db.Groups
                            where p.Name.Contains(searchString)
                            orderby p.Name ascending
                            select p).ToList();
                
            return groups;
        }

        /// <summary>
        /// Get a list of all groups where the user is an admin.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Group> GetGroupsUserIsAdmin(string id)
        {
            var groups = (from p in m_db.Groups
                          where p.Administrator.Id == id
                          orderby p.Name ascending
                          select p).ToList();
            return groups;
        }

        /// <summary>
        /// Get a list of all groups the user is a member of.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Group> GetGroupsUserIsIn(ApplicationUser user)
        {
            var groups = (from p in m_db.Groups
                          where p.Administrator.Id != user.Id
                          from r in p.Members
                          where r.Id == user.Id
                          orderby p.Name ascending
                          select p).ToList();
            return groups;
        }

        /// <summary>
        /// Get a specified group.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Group GetGroupById(int id)
        {
            var group = (from p in m_db.Groups
                        where p.Id == id
                        select p).SingleOrDefault();

            return group;
        }

        /// <summary>
        /// Get a list of all members within the specified group.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ApplicationUser> GetUsersByGroupId(int id)
        {
            var group = GetGroupById(id);
            return group.Members;
        }

        /// <summary>
        /// Returns true if the specified user is a member of the specified group, otherwise returns false.
        /// </summary>
        /// <param name="GroupId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds a group to the system.
        /// </summary>
        /// <param name="group"></param>
        public void AddGroup(Group group)
        {
            m_db.Groups.Add(group);
            m_db.SaveChanges();
        }

        /// <summary>
        /// Updates the specified group.
        /// </summary>
        /// <param name="group"></param>
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

        /// <summary>
        /// Adds the specified member to the specified group.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="user"></param>
        public void AddMemberToGroup(Group group, ApplicationUser user)
        {
            group.Members.Add(user);
            m_db.SaveChanges();
        }

        /// <summary>
        /// Removes the specified member from the specified group.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="user"></param>
        public void RemoveMemberFromGroup(Group group, ApplicationUser user)
        {
            group.Members.Remove(user);
            m_db.SaveChanges();
        }

    }
}