namespace UNIverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blb : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Comment", newName: "Comments");
            RenameTable(name: "dbo.Department", newName: "Departments");
            RenameTable(name: "dbo.University", newName: "Universities");
            RenameTable(name: "dbo.Group", newName: "Groups");
            RenameTable(name: "dbo.Post", newName: "Posts");
            RenameTable(name: "dbo.FriendRequest", newName: "FriendRequests");
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            RenameColumn(table: "dbo.AspNetUserClaims", name: "UserId", newName: "User_Id");
            RenameIndex(table: "dbo.AspNetUserClaims", name: "IX_UserId", newName: "IX_User_Id");
            DropPrimaryKey("dbo.AspNetUserLogins");
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Groups", "GroupPicturePath", c => c.String());
            AddColumn("dbo.Posts", "ImagePath", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetRoles", "Name", c => c.String(nullable: false));
            AddPrimaryKey("dbo.AspNetUserLogins", new[] { "UserId", "LoginProvider", "ProviderKey" });
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "LockoutEndDateUtc");
            DropColumn("dbo.AspNetUsers", "LockoutEnabled");
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
            DropColumn("dbo.FriendRequests", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FriendRequests", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
            DropPrimaryKey("dbo.AspNetUserLogins");
            AlterColumn("dbo.AspNetRoles", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 256));
            DropColumn("dbo.Posts", "ImagePath");
            DropColumn("dbo.Groups", "GroupPicturePath");
            DropColumn("dbo.AspNetUsers", "Discriminator");
            AddPrimaryKey("dbo.AspNetUserLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            RenameIndex(table: "dbo.AspNetUserClaims", name: "IX_User_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.AspNetUserClaims", name: "User_Id", newName: "UserId");
            CreateIndex("dbo.AspNetRoles", "Name", unique: true, name: "RoleNameIndex");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            RenameTable(name: "dbo.FriendRequests", newName: "FriendRequest");
            RenameTable(name: "dbo.Posts", newName: "Post");
            RenameTable(name: "dbo.Groups", newName: "Group");
            RenameTable(name: "dbo.Universities", newName: "University");
            RenameTable(name: "dbo.Departments", newName: "Department");
            RenameTable(name: "dbo.Comments", newName: "Comment");
        }
    }
}
