﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNIverse.Models.Entities
{
    public class FriendRequest
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public bool IsAccepted { get; set; }

        public DateTime RequestDate { get; set; }

        public virtual ApplicationUser Sender { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
    }
}