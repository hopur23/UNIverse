using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UNIverse.Models;
using UNIverse.Models.Entities;

namespace UNIverse.Services
{
    public class FriendService
    {
        private ApplicationDbContext m_db;

        public FriendService(ApplicationDbContext context)
        {
            m_db = context;
        }

        public FriendRequest GetFriendRequestById(int id)
        {
            var request = (from r in m_db.FriendRequests
                           where r.Id == id
                           select r).SingleOrDefault();

            return request;
        }

        // Get all friend requests the specified user is involved in (either
        // sender or receiver)
        public IEnumerable<FriendRequest> GetFriendRequestsByUser(string id)
        {
            var requests = (from r in m_db.FriendRequests
                            where (r.Receiver.Id == id || r.Sender.Id == id)
                            select r).ToList();
            return requests;
        }

        public void AddFriendRequest(FriendRequest request)
        {
            m_db.FriendRequests.Add(request);
            m_db.SaveChanges();
        }

        public void UpdateFriendRequest(FriendRequest request)
        {
            m_db.FriendRequests.Attach(request);
            m_db.Entry(request).State = EntityState.Modified;
            m_db.SaveChanges();
        }
    }
}