using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNIverse.Models;

namespace UNIverse.Services
{
    public class PostService
    {
        private ApplicationDbContext m_db;

        public PostService(ApplicationDbContext context)
        {
            m_db = context;
        }

        public List<Post> GetAllPosts()
        {
            var posts = (from p in m_db.Posts
                            orderby p.DateCreated descending
                            select p).ToList();
            return posts;    
        }

        public Post GetPostById(int id)
        {
            var post = (from p in m_db.Posts
                        where p.Id == id
                        select p).SingleOrDefault();

            return post;
        }

        public void AddComment(Comment comment)
        {
            m_db.Comments.Add(comment);
            m_db.SaveChanges();
        }

        public void AddPost(Post post)
        {
            m_db.Posts.Add(post);
            m_db.SaveChanges();
        }
    }
}