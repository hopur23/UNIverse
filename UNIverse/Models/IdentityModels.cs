﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using UNIverse.Models.Entities;

namespace UNIverse.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public DateTime Birthday { get; set; }
        public string Description { get; set; }
        public string ProfilePicturePath { get; set; }

        public virtual Department Department { get; set; }
        public virtual List<ApplicationUser> Friends { get; set; }
        public virtual List<Group> Groups { get; set; }
        public virtual List<Post> Posts { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Singleton pattern á DbContext.
        // Tryggir að við höfum alltaf bara eitt instance af contextinu og að við getum nálgast það beint úr ServiceWrapper
        private static ApplicationDbContext instance;

        public static ApplicationDbContext Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ApplicationDbContext();
                }
                return instance;
            }
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Group>()
                .HasMany(c => c.Members).WithMany(i => i.Groups)
                .Map(t => t.MapLeftKey("GroupID")
                    .MapRightKey("UserID")
                    .ToTable("GroupMembers"));

           /* modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Friends).WithMany(i => i.Friends)
                .Map(t => t.MapLeftKey("User1ID")
                    .MapRightKey("User2ID")
                    .ToTable("Friends"));*/
        }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}