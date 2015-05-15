namespace UNIverse.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UNIverse.Models;
    using UNIverse.Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<UNIverse.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UNIverse.Models.ApplicationDbContext context)
        {
            #region Create administrator
            /***Seeding the administrator***/
            // Specify user and role name
            var userName = "Admin";
            var userId = Guid.NewGuid().ToString();
            var roleName = "Administrator";
            var roleId = Guid.NewGuid().ToString();

            // Create the role.
            if(!context.Roles.Any(r => r.Name == roleName))
            {
                var userRole = new IdentityRole() { Name = roleName, Id = roleId };
                context.Roles.Add(userRole);
            }

            var hasher = new PasswordHasher();

            // Create the user.
            if(!context.Roles.Any(u => u.Name == userName))
            {
                var user = new ApplicationUser()
                {
                    UserName = userName,
                    PasswordHash = hasher.HashPassword(userName),
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.Roles.Add(new IdentityUserRole { RoleId = roleId, UserId = userId });
                context.Users.Add(user);
            }
            #endregion

            #region Create University with groups

            var university = new University()
            {
                Name = "Reykjavik University",
                EmailEnding = "@ru.is",
                Description = "Reykjavík University is the best university in the world!",
            };

            if(!context.Universities.Any(u => u.Name == "Reykjavik University"))
            {
                // Create the university
                

                // Add groups to university
                List<Group> groups = new List<Group>() {
                                        new Group() {
                                            Name = "School of Science and Engineering",
                                            Description = "The science and engineering department",
                                            GroupPicturePath = "/Content/images/no-group-img.jpg",
                                            ParentUniversity = university
                                        },
                                        new Group() {
                                            Name = "School of Law",
                                            Description = "The law department",
                                            GroupPicturePath = "/Content/images/no-group-img.jpg",
                                            ParentUniversity = university
                                        },
                                        new Group() {
                                            Name = "School of Business",
                                            Description = "The business department",
                                            GroupPicturePath = "/Content/images/no-group-img.jpg",
                                            ParentUniversity = university
                                        },
                                        new Group() {
                                            Name = "Nemendafélagið Tvíund",
                                            Description = "Tvíund rocks!",
                                            GroupPicturePath = "/Content/images/no-group-img.jpg",
                                            ParentUniversity = university
                                        },
                                        new Group() {
                                            Name = "Nemendafélagið Lögrétta",
                                            Description = "Mostly snobs",
                                            GroupPicturePath = "/Content/images/no-group-img.jpg",
                                            ParentUniversity = university
                                        },
                                        new Group() {
                                            Name = "/sys/tur",
                                            Description = "Nerdy girls",
                                            GroupPicturePath = "/Content/images/no-group-img.jpg",
                                            ParentUniversity = university
                                        }
                                    };
                university.Groups = groups;

                context.Universities.Add(university);
                context.SaveChanges();
            }
            
            #endregion

            #region Create Users

            if(!context.Users.Any(u => u.UserName == "ragnarmh13@ru.is"))
            {
                var ragnar = new ApplicationUser()
                {
                    UserName = "ragnar@ru.is",
                    PasswordHash = hasher.HashPassword("ragnar.123"),
                    FirstName = "Ragnar",
                    LastName = "Halldorsson",
                    Groups = new List<Group>(),
                    FriendRequests = new List<FriendRequest>(),
                    Posts = new List<Post>(),
                    Description = "A pretty cool guy!",
                    Email = "ragnar@ru.is",
                    ProfilePicturePath = "/Content/images/no-profile.jpg",
                    University = university
                };
                context.Users.AddOrUpdate(ragnar);
            
                var berglind = new ApplicationUser()
                {
                    UserName = "berglind@ru.is",
                    PasswordHash = hasher.HashPassword("berglind.123"),
                    FirstName = "Berglind",
                    LastName = "Björnsdóttir",
                    Groups = new List<Group>(),
                    FriendRequests = new List<FriendRequest>(),
                    Posts = new List<Post>(),
                    Description = "A pretty cool gal!",
                    Email = "berglind@ru.is",
                    ProfilePicturePath = "/Content/images/no-profile.jpg",
                    University = university
                };
                context.Users.AddOrUpdate(berglind);

                var elisa = new ApplicationUser()
                {
                    UserName = "elisa@ru.is",
                    PasswordHash = hasher.HashPassword("elisa.123"),
                    FirstName = "Elísa",
                    LastName = "Hermundardóttir",
                    Groups = new List<Group>(),
                    FriendRequests = new List<FriendRequest>(),
                    Posts = new List<Post>(),
                    Description = "A pretty cool gal!",
                    Email = "elisa@ru.is",
                    ProfilePicturePath = "/Content/images/no-profile.jpg",
                    University = university
                };
                context.Users.AddOrUpdate(elisa);

                var thorgerdur = new ApplicationUser()
                {
                    UserName = "thorgerdur@ru.is",
                    PasswordHash = hasher.HashPassword("thorgerdur.123"),
                    FirstName = "Þorgerður",
                    LastName = "Eiríksdóttir",
                    Groups = new List<Group>(),
                    FriendRequests = new List<FriendRequest>(),
                    Posts = new List<Post>(),
                    Description = "A pretty cool gal!",
                    Email = "thorgerdur@ru.is",
                    ProfilePicturePath = "/Content/images/no-profile.jpg",
                    University = university
                };
                context.Users.AddOrUpdate(thorgerdur);

                var arnheidur = new ApplicationUser()
                {
                    UserName = "arnheidur@ru.is",
                    PasswordHash = hasher.HashPassword("arnheidur.123"),
                    FirstName = "Arnheiður",
                    LastName = "Sigurðardóttir",
                    Groups = new List<Group>(),
                    FriendRequests = new List<FriendRequest>(),
                    Posts = new List<Post>(),
                    Description = "A pretty cool gal!",
                    Email = "arnheidur@ru.is",
                    ProfilePicturePath = "/Content/images/no-profile.jpg",
                    University = university
                };
                context.Users.AddOrUpdate(arnheidur);

                #region Add friend request
                var request = new FriendRequest() {
                    Sender = ragnar,
                    SenderId = ragnar.Id,
                    Receiver = elisa,
                    ReceiverId = elisa.Id,
                    IsAccepted = true,
                    RequestDate = DateTime.Now
                };

                context.FriendRequests.AddOrUpdate(request);
                #endregion
            }
            
            #endregion

            context.SaveChanges();
        }
    }
}
