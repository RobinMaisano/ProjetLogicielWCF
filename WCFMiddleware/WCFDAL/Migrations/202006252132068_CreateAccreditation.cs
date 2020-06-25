namespace WCFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAccreditation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Privileges", "User_ID", "dbo.Users");
            DropIndex("dbo.Privileges", new[] { "User_ID" });
            CreateTable(
                "dbo.UserPrivileges",
                c => new
                    {
                        User_ID = c.Int(nullable: false),
                        Privilege_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_ID, t.Privilege_ID })
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .ForeignKey("dbo.Privileges", t => t.Privilege_ID, cascadeDelete: true)
                .Index(t => t.User_ID)
                .Index(t => t.Privilege_ID);
            
            DropColumn("dbo.Privileges", "User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Privileges", "User_ID", c => c.Int());
            DropForeignKey("dbo.UserPrivileges", "Privilege_ID", "dbo.Privileges");
            DropForeignKey("dbo.UserPrivileges", "User_ID", "dbo.Users");
            DropIndex("dbo.UserPrivileges", new[] { "Privilege_ID" });
            DropIndex("dbo.UserPrivileges", new[] { "User_ID" });
            DropTable("dbo.UserPrivileges");
            CreateIndex("dbo.Privileges", "User_ID");
            AddForeignKey("dbo.Privileges", "User_ID", "dbo.Users", "ID");
        }
    }
}
