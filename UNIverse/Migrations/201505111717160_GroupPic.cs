namespace UNIverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupPic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Group", "GroupPicturePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Group", "GroupPicturePath");
        }
    }
}
