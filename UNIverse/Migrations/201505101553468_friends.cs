namespace UNIverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friends : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        receiverId = c.String(nullable: false, maxLength: 128),
                        requesterId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.receiverId, t.requesterId })
                .ForeignKey("dbo.AspNetUsers", t => t.receiverId)
                .ForeignKey("dbo.AspNetUsers", t => t.requesterId)
                .Index(t => t.receiverId)
                .Index(t => t.requesterId);
            
            DropColumn("dbo.AspNetUsers", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Friends", "requesterId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "receiverId", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "requesterId" });
            DropIndex("dbo.Friends", new[] { "receiverId" });
            DropTable("dbo.Friends");
            CreateIndex("dbo.AspNetUsers", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
