namespace Library.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _010 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ksiazki", "Amount", c => c.Int(nullable: false));
            DropColumn("dbo.Ksiazki", "Availability");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ksiazki", "Availability", c => c.Int(nullable: false));
            DropColumn("dbo.Ksiazki", "Amount");
        }
    }
}
