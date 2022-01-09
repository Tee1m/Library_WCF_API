namespace LibraryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Klienci", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Klienci", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Klienci", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Klienci", "Address", c => c.String());
            AlterColumn("dbo.Klienci", "Surname", c => c.String());
            AlterColumn("dbo.Klienci", "Name", c => c.String());
        }
    }
}
