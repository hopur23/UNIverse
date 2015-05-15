using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNIverse.Models;
using UNIverse.Models.ViewModels;

namespace UNIverse.Services
{
    public class PostService
    {
        private ApplicationDbContext m_db;

        public PostService(ApplicationDbContext context)
        {
            m_db = context;
        }

        /// <summary>
        /// Gets a list of all posts in the system, ordered by date.
        /// </summary>
        /// <returns></returns>
        public List<Post> GetAllPosts()
        {
            var posts = (from p in m_db.Posts
                            orderby p.DateCreated descending
                            select p).ToList();
            return posts;    
        }

        /// <summary>
        /// Gets a list of all posts that should be displayed on the wall of the specified user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Post> GetAllPostsForProfileWall(string id)
        {
            var posts = (from p in m_db.Posts
                         where (p.ParentGroup == null) && (p.Author.Id == id)
                         orderby p.DateCreated descending
                         select p).ToList();
            return posts;
        }

        /// <summary>
        /// Gets a list of all posts from user's friends.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Post> GetAllPostsFromFriends(string id)
        {
            var posts = (from p in m_db.Posts
                         from r in m_db.FriendRequests
                         // My own posts
                         where (p.ParentGroup == null && p.Author.Id == id)
                         // My friends wall posts
                         || ((p.ParentGroup == null) && (
                         ((r.SenderId == p.Author.Id) && (r.ReceiverId == id)) 
                         || ((r.ReceiverId == p.Author.Id) && (r.SenderId == id)))
                         && (r.IsAccepted == true)
                         )
                         //orderby p.DateCreated descending
                         select p).Distinct().OrderByDescending(m=>m.DateCreated).ToList();
            return posts;
        }

        /// <summary>
        /// Gets a list of all posts that have the specified tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public List<Post> PostsByTag(string tag)
        {
            var posts = (from p in m_db.Posts
                         where (p.Tag == tag) && (p.ParentGroup == null)
                         orderby p.DateCreated descending
                         select p).ToList();
            return posts;
        }

        /// <summary>
        /// Gets a post with the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Post GetPostById(int id)
        {
            var post = (from p in m_db.Posts
                        where p.Id == id
                        select p).SingleOrDefault();

            return post;
        }

        /// <summary>
        /// Gets a comment with the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Comment GetCommentById(int id)
        {
            var comment = (from p in m_db.Comments
                        where p.Id == id
                        select p).SingleOrDefault();

            return comment;
        }

        /// <summary>
        /// Updates the specified post in the system.
        /// </summary>
        /// <param name="post"></param>
        public void EditPost(Post post)
        {
            Post g = ServiceWrapper.PostService.GetPostById(post.Id);
            if (g != null)
            {
                g.Body = post.Body;
                g.ImagePath = post.ImagePath;
                g.Tag = post.Tag;
                m_db.SaveChanges();
            }
        }

        /// <summary>
        /// Adds the specified comment to the system
        /// </summary>
        /// <param name="comment"></param>
        public void AddComment(Comment comment)
        {
            m_db.Comments.Add(comment);
            m_db.SaveChanges();
        }

        /// <summary>
        /// Removes the specified comment from the system
        /// </summary>
        /// <param name="comment"></param>
        public void DeleteComment(Comment comment)
        {
            m_db.Comments.Remove(comment);
            m_db.SaveChanges();
        }

        /// <summary>
        /// Adds the specified post to the system.
        /// </summary>
        /// <param name="post"></param>
        public void AddPost(Post post)
        {
            m_db.Posts.Add(post);
            m_db.SaveChanges();
        }

        /// <summary>
        /// Removes the specified post from the system.
        /// </summary>
        /// <param name="post"></param>
        public void DeletePost(Post post)
        {
            // Iterate through the post's comments to delete them from the database.
            if (post.Comments != null)
            {
                foreach (var item in post.Comments.ToList())
                {
                    m_db.Comments.Remove(item);
                }
            }
            m_db.Posts.Remove(post);
            m_db.SaveChanges();
        }
    }
}