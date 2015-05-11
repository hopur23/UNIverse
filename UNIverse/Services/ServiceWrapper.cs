using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNIverse.Models;

namespace UNIverse.Services
{
    public static class ServiceWrapper
    {
        private static ApplicationDbContext context = ApplicationDbContext.Instance;

        private static PostService postSvc;
        public static PostService PostService
        {
            get
            {
                if(postSvc == null)
                {
                    postSvc = new PostService(context);
                }
                return postSvc;
            }
        }

        private static UserService userSvc;
        public static UserService UserService
        {
            get
            {
                if(userSvc == null)
                {
                    userSvc = new UserService(context);
                }
                return userSvc;
            }
        }

        private static GroupService groupSvc;
        public static GroupService GroupService
        {
            get
            {
                if(groupSvc == null)
                {
                    groupSvc = new GroupService(context);
                }
                return groupSvc;
            }
        }

        private static FriendService friendSvc;
        public static FriendService FriendService
        {
            get
            {
                if (friendSvc == null)
                {
                    friendSvc = new FriendService(context);
                }
                return friendSvc;
            }
        }
    }
}