namespace UNIverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusPic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "ImagePath");
        }
    }
}
