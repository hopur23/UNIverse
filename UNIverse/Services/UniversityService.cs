using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNIverse.Models;

namespace UNIverse.Services
{
    public class UniversityService
    {
        private ApplicationDbContext m_db;

        public UniversityService(ApplicationDbContext context)
        {
            m_db = context;
        }

        /// <summary>
        /// Returns a list of strings containing all valid email endings
        /// </summary>
        /// <returns></returns>
        public List<string> GetUniEndings()
        {
            var endings = (from u in m_db.Universities
                        select u.EmailEnding).ToList();
            return endings;
        }

        /// <summary>
        /// Searches for universities with the specified email ending.
        /// Returns the University entity if found, or null if not found.
        /// </summary>
        /// <param name="ending"></param>
        /// <returns></returns>
        public University GetUniByEnding(string ending)
        {
            var uni = (from u in m_db.Universities
                       where u.EmailEnding == ending
                       select u).SingleOrDefault();
            return uni;
        }
    }
}