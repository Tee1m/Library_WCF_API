namespace Library.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _008 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Klienci", "TelephoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Klienci", "TelephoneNumber", c => c.Int(nullable: false));
        }
    }
}
