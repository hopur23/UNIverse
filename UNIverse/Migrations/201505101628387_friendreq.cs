namespace UNIverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendreq : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friends", "receiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "requesterId", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "receiverId" });
            DropIndex("dbo.Friends", new[] { "requesterId" });
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.String(maxLength: 128),
                        ReceiverId = c.String(maxLength: 128),
                        IsAccepted = c.Boolean(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ReceiverId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.ApplicationUser_Id);
            
            DropTable("dbo.Friends");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        receiverId = c.String(nullable: false, maxLength: 128),
                        requesterId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.receiverId, t.requesterId });
            
            DropForeignKey("dbo.FriendRequests", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "ReceiverId", "dbo.AspNetUsers");
            DropIndex("dbo.FriendRequests", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FriendRequests", new[] { "ReceiverId" });
            DropIndex("dbo.FriendRequests", new[] { "SenderId" });
            DropTable("dbo.FriendRequests");
            CreateIndex("dbo.Friends", "requesterId");
            CreateIndex("dbo.Friends", "receiverId");
            AddForeignKey("dbo.Friends", "requesterId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friends", "receiverId", "dbo.AspNetUsers", "Id");
        }
    }
}
