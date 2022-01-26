namespace Library.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _007 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ksiazki", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ksiazki", "Price", c => c.Single(nullable: false));
        }
    }
}
