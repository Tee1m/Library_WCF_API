namespace Library.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ksiazki", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Wypozyczenia", "DateOfBorrow", c => c.DateTime(nullable: false));
            AddColumn("dbo.Wypozyczenia", "Return", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Ksiazki", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.Ksiazki", "Name");
            DropColumn("dbo.Wypozyczenia", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wypozyczenia", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Ksiazki", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Ksiazki", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Wypozyczenia", "Return");
            DropColumn("dbo.Wypozyczenia", "DateOfBorrow");
            DropColumn("dbo.Ksiazki", "Title");
        }
    }
}
