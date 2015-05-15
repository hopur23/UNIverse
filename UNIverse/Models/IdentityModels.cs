using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "You must have a first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You must have a last name.")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ProfilePicturePath { get; set; }

        public virtual University University { get; set; }
        public virtual List<Group> Groups { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual List<FriendRequest> FriendRequests { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Keep an instance alive from the first get for the duration of the program. Ensures no dual-context problems.
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
        public DbSet<FriendRequest> FriendRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>()
                 .HasMany<ApplicationUser>(u => u.Members)
                 .WithMany(i => i.Groups)
                .Map(t => t.MapLeftKey("GroupID")
                    .MapRightKey("UserID")
                    .ToTable("GroupMembers"));

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.FriendRequests)
                .WithRequired(f => f.Sender)
                .HasForeignKey(f => f.SenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FriendRequest>()
                .HasKey(f => new { f.SenderId, f.ReceiverId });

            modelBuilder.Entity<FriendRequest>()
                .HasRequired(f => f.Receiver)
                .WithMany()
                .HasForeignKey(f => f.ReceiverId);
        }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}