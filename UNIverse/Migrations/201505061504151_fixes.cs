namespace UNIverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Tag = c.String(),
                        content = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                        ParentGroup_GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Groups", t => t.ParentGroup_GroupId)
                .Index(t => t.Owner_Id)
                .Index(t => t.ParentGroup_GroupId);
            
            AddColumn("dbo.Comments", "Parent_PostId", c => c.Int());
            AddColumn("dbo.Comments", "Owner_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Groups", "ParentUniversity_UniversityId", c => c.Int());
            AddColumn("dbo.Groups", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Universities", "Description", c => c.String());
            AddColumn("dbo.AspNetUsers", "ProfilePicturePath", c => c.String());
            AddColumn("dbo.AspNetUsers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Group_GroupId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Group_GroupId1", c => c.Int());
            AddColumn("dbo.AspNetUsers", "University_UniversityId", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false));
            CreateIndex("dbo.Comments", "Parent_PostId");
            CreateIndex("dbo.Comments", "Owner_Id");
            CreateIndex("dbo.AspNetUsers", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "Group_GroupId");
            CreateIndex("dbo.AspNetUsers", "Group_GroupId1");
            CreateIndex("dbo.AspNetUsers", "University_UniversityId");
            CreateIndex("dbo.Groups", "ParentUniversity_UniversityId");
            CreateIndex("dbo.Groups", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Group_GroupId", "dbo.Groups", "GroupId");
            AddForeignKey("dbo.AspNetUsers", "Group_GroupId1", "dbo.Groups", "GroupId");
            AddForeignKey("dbo.Groups", "ParentUniversity_UniversityId", "dbo.Universities", "UniversityId");
            AddForeignKey("dbo.AspNetUsers", "University_UniversityId", "dbo.Universities", "UniversityId");
            AddForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "Parent_PostId", "dbo.Posts", "PostId");
            AddForeignKey("dbo.Comments", "Owner_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.UserPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        content = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostId);
            
            DropForeignKey("dbo.Comments", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ParentGroup_GroupId", "dbo.Groups");
            DropForeignKey("dbo.Posts", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Parent_PostId", "dbo.Posts");
            DropForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "University_UniversityId", "dbo.Universities");
            DropForeignKey("dbo.Groups", "ParentUniversity_UniversityId", "dbo.Universities");
            DropForeignKey("dbo.AspNetUsers", "Group_GroupId1", "dbo.Groups");
            DropForeignKey("dbo.AspNetUsers", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ParentGroup_GroupId" });
            DropIndex("dbo.Posts", new[] { "Owner_Id" });
            DropIndex("dbo.Groups", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Groups", new[] { "ParentUniversity_UniversityId" });
            DropIndex("dbo.AspNetUsers", new[] { "University_UniversityId" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_GroupId1" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_GroupId" });
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Comments", new[] { "Owner_Id" });
            DropIndex("dbo.Comments", new[] { "Parent_PostId" });
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String());
            DropColumn("dbo.AspNetUsers", "University_UniversityId");
            DropColumn("dbo.AspNetUsers", "Group_GroupId1");
            DropColumn("dbo.AspNetUsers", "Group_GroupId");
            DropColumn("dbo.AspNetUsers", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "ProfilePicturePath");
            DropColumn("dbo.Universities", "Description");
            DropColumn("dbo.Groups", "ApplicationUser_Id");
            DropColumn("dbo.Groups", "ParentUniversity_UniversityId");
            DropColumn("dbo.Comments", "Owner_Id");
            DropColumn("dbo.Comments", "Parent_PostId");
            DropTable("dbo.Posts");
        }
    }
}
