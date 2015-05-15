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

        // Get all friend requests the specified user is involved in (either
        // sender or receiver)
        /// <summary>
        /// Gets all friend requests the specified user is involved in. That is sender, receiver, pending and not pending.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        public IEnumerable<FriendRequest> GetAllFriendRequests(string id)
        {
            var requests = (from r in m_db.FriendRequests
                            where (r.Receiver.Id == id || r.Sender.Id == id)
                            select r).ToList();
            return requests;
        }

        /// <summary>
        /// Gets a list of all received friend requests that are still pending, ordered by newest request first.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        public List<FriendRequest> GetReceivedFriendRequests(string id)
        {
            var requests = (from r in m_db.FriendRequests
                            where (r.Receiver.Id == id && r.IsAccepted == false)
                            orderby r.RequestDate descending
                            select r).ToList();
            return requests;
        }

        /// <summary>
        /// Finds the friend requests between User 1 and User 2. Can be either pending or accepted.
        /// Returns the request if found, else null.
        /// </summary>
        /// <param name="user1Id">User 1 ID</param>
        /// <param name="user2Id">User 2 ID</param>
        /// <returns></returns>
        public FriendRequest FindRequestBetween(string user1Id, string user2Id)
        {
            var request = (from r in m_db.FriendRequests
                           where (r.Sender.Id == user1Id && r.Receiver.Id == user2Id)
                              || (r.Sender.Id == user2Id && r.Receiver.Id == user1Id)
                           select r).SingleOrDefault();
            return request;
        }

        /// <summary>
        /// Gets a list of all sent friend requests that are still pending, ordered by newest request first.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        public List<FriendRequest> GetSentFriendRequests(string id)
        {
            var requests = (from r in m_db.FriendRequests
                            where (r.Sender.Id == id && r.IsAccepted == false)
                            orderby r.RequestDate descending
                            select r).ToList();
            return requests;
        }

        public List<ApplicationUser> GetPendingRequests(string id)
        {
            var pending = (from r in m_db.FriendRequests
                            where (r.ReceiverId == id) && 
                            (r.IsAccepted == false)
                            select r.Sender).ToList();
            return pending;
        }

        public List<ApplicationUser> GetSentRequests(string id)
        {
            var pending = (from r in m_db.FriendRequests
                           where (r.SenderId == id) &&
                           (r.IsAccepted == false)
                           select r.Receiver).ToList();
            return pending;
        }

        /// <summary>
        /// Gets a list of all friends for a user, unordered.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ApplicationUser> GetFriendsForUser(string id)
        {
            var requests = (from r in m_db.FriendRequests
                            where (r.ReceiverId == id || r.SenderId == id)
                            && r.IsAccepted == true
                            select r).ToList();

            var friends = new List<ApplicationUser>();

            // Ítra gegnum öll friend requestin. Ef að ID notandans er í Sender, þá er Receiver vinur hans, og öfugt.
            foreach(var request in requests)
            {
                if(request.SenderId == id) {
                    friends.Add(request.Receiver);
                }
                else {
                    friends.Add(request.Sender);
                }
            }
            return friends;
        }

        /// <summary>
        /// Adds a friend request to the system.
        /// </summary>
        /// <param name="request"></param>
        public void AddFriendRequest(FriendRequest request)
        {
            m_db.FriendRequests.Add(request);
            m_db.SaveChanges();
        }

        /// <summary>
        /// Updates a friend request in the system.
        /// </summary>
        /// <param name="request"></param>
        public void UpdateFriendRequest(FriendRequest request)
        {
            m_db.FriendRequests.Attach(request);
            m_db.Entry(request).State = EntityState.Modified;
            m_db.SaveChanges();
        }

        /// <summary>
        /// Removes a friend request from the system.
        /// </summary>
        /// <param name="request"></param>
        public void RemoveFriendRequest(FriendRequest request)
        {
            m_db.FriendRequests.Remove(request);
            m_db.SaveChanges();
        }
    }
}