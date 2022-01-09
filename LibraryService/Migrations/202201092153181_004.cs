namespace LibraryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _004 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Wypozyczenia", "Return", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Wypozyczenia", "Return", c => c.DateTime(nullable: false));
        }
    }
}
