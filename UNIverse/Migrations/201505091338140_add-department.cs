namespace UNIverse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddepartment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentUniversity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Universities", t => t.ParentUniversity_Id)
                .Index(t => t.ParentUniversity_Id);
            
            AddColumn("dbo.AspNetUsers", "Department_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Department_Id");
            AddForeignKey("dbo.AspNetUsers", "Department_Id", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Departments", "ParentUniversity_Id", "dbo.Universities");
            DropIndex("dbo.Departments", new[] { "ParentUniversity_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Department_Id" });
            DropColumn("dbo.AspNetUsers", "Department_Id");
            DropTable("dbo.Departments");
        }
    }
}
