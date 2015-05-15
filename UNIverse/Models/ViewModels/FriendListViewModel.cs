using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNIverse.Models;
using UNIverse.Models.Entities;

namespace UNIverse.Models.ViewModels
{
    public class FriendListViewModel
    {
        public List<ApplicationUser> Friends { get; set; }
        public List<ApplicationUser> PendingSent { get; set; }
        public List<ApplicationUser> PendingReceived { get; set; }
        public List<FriendRequest> ReceivedRequests { get; set; }
        public List<FriendRequest> SentRequests { get; set; }
    }
}