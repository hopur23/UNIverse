namespace UNIverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.CommentId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        UniversityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmailEnding = c.String(),
                    })
                .PrimaryKey(t => t.UniversityId);
            
            AddColumn("dbo.UserPosts", "PostId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "Description", c => c.String());
            DropPrimaryKey("dbo.UserPosts");
            AddPrimaryKey("dbo.UserPosts", "PostId");
            DropColumn("dbo.UserPosts", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPosts", "ID", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.UserPosts");
            AddPrimaryKey("dbo.UserPosts", "ID");
            DropColumn("dbo.AspNetUsers", "Description");
            DropColumn("dbo.AspNetUsers", "Birthday");
            DropColumn("dbo.UserPosts", "PostId");
            DropTable("dbo.Universities");
            DropTable("dbo.Groups");
            DropTable("dbo.Comments");
        }
    }
}
