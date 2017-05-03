namespace CrudApp_Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class end : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "start", c => c.DateTime());
            AlterColumn("dbo.Notes", "end", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "end", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Notes", "start", c => c.DateTime(nullable: false));
        }
    }
}
