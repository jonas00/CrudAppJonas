namespace CrudApp_Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Text = c.String(nullable: false),
                        Autor = c.String(nullable: false, maxLength: 30),
                        Tag = c.String(nullable: false),
                        Email = c.String(maxLength: 40),
                        Archiv = c.Boolean(nullable: false),
                        UserId = c.String(),
                        Created = c.DateTime(nullable: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notes");
        }
    }
}
