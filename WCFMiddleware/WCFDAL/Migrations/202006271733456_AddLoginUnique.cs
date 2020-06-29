namespace WCFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoginUnique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Users", "Login", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Login" });
            AlterColumn("dbo.Users", "Login", c => c.String());
        }
    }
}
