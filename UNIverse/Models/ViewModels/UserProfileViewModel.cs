﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using UNIverse.Models.Entities;
using System.ComponentModel.DataAnnotations;


namespace UNIverse.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public string userId { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ProfilePicturePath { get; set; }
        public bool IsMyFriend { get; set; }
        public bool HasRequestFromMe { get; set; }
        public bool HaveRequestFromHim { get; set; }

        public List<Post> Posts { get; set; }
        public List<FriendRequest> SentFriendRequests { get; set; }
        public List<FriendRequest> ReceivedFriendRequests { get; set; }
        public List<ApplicationUser> Friends { get; set; }
        public List<Group> Groups { get; set; }
        public University School { get; set; }

    }
}