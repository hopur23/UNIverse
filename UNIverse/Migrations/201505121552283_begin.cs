namespace UNIverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class begin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Parent_Id = c.Int(),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.Parent_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Birthday = c.DateTime(),
                        Description = c.String(),
                        ProfilePicturePath = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        University_Id = c.Int(),
                        Department_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.University", t => t.University_Id)
                .ForeignKey("dbo.Department", t => t.Department_Id)
                .Index(t => t.University_Id)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentUniversity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.University", t => t.ParentUniversity_Id)
                .Index(t => t.ParentUniversity_Id);
            
            CreateTable(
                "dbo.University",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        EmailEnding = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Administrator_Id = c.String(maxLength: 128),
                        ParentUniversity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Administrator_Id)
                .ForeignKey("dbo.University", t => t.ParentUniversity_Id)
                .Index(t => t.Administrator_Id)
                .Index(t => t.ParentUniversity_Id);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Tag = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                        ParentGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Group", t => t.ParentGroup_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.ParentGroup_Id);
            
            CreateTable(
                "dbo.FriendRequest",
                c => new
                    {
                        SenderId = c.String(nullable: false, maxLength: 128),
                        ReceiverId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.SenderId, t.ReceiverId })
                .ForeignKey("dbo.AspNetUsers", t => t.ReceiverId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupID, t.UserID })
                .ForeignKey("dbo.Group", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequest", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequest", "ReceiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Department_Id", "dbo.Department");
            DropForeignKey("dbo.AspNetUsers", "University_Id", "dbo.University");
            DropForeignKey("dbo.Post", "ParentGroup_Id", "dbo.Group");
            DropForeignKey("dbo.Comment", "Parent_Id", "dbo.Post");
            DropForeignKey("dbo.Post", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Group", "ParentUniversity_Id", "dbo.University");
            DropForeignKey("dbo.GroupMembers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupMembers", "GroupID", "dbo.Group");
            DropForeignKey("dbo.Group", "Administrator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Department", "ParentUniversity_Id", "dbo.University");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GroupMembers", new[] { "UserID" });
            DropIndex("dbo.GroupMembers", new[] { "GroupID" });
            DropIndex("dbo.FriendRequest", new[] { "ReceiverId" });
            DropIndex("dbo.FriendRequest", new[] { "SenderId" });
            DropIndex("dbo.Post", new[] { "ParentGroup_Id" });
            DropIndex("dbo.Post", new[] { "Author_Id" });
            DropIndex("dbo.Group", new[] { "ParentUniversity_Id" });
            DropIndex("dbo.Group", new[] { "Administrator_Id" });
            DropIndex("dbo.Department", new[] { "ParentUniversity_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Department_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "University_Id" });
            DropIndex("dbo.Comment", new[] { "Author_Id" });
            DropIndex("dbo.Comment", new[] { "Parent_Id" });
            DropTable("dbo.GroupMembers");
            DropTable("dbo.FriendRequest");
            DropTable("dbo.Post");
            DropTable("dbo.Group");
            DropTable("dbo.University");
            DropTable("dbo.Department");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comment");
        }
    }
}
