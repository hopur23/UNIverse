using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNIverse.Models;

namespace UNIverse.Services
{
    public class ServiceWrapper
    {
        private static ApplicationDbContext context = ApplicationDbContext.Instance;

        private static ServiceWrapper instance;
        public static ServiceWrapper Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ServiceWrapper();
                }
                return instance;
            }
        }

        private static PostService postSvc;
        public PostService PostService
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
        public UserService UserService
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
        public GroupService GroupService
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
    }
}