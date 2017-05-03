namespace CrudApp_Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "start", c => c.DateTime(nullable: true));
            AlterColumn("dbo.Notes", "end", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "end", c => c.DateTime());
            AlterColumn("dbo.Notes", "start", c => c.DateTime());
        }
    }
}
