namespace Library.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _002 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ksiazki",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        AuthorName = c.String(nullable: false),
                        AuthorSurname = c.String(nullable: false),
                        Availability = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wypozyczenia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Books_Id = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ksiazki", t => t.Books_Id, cascadeDelete: true)
                .ForeignKey("dbo.Klienci", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Books_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wypozyczenia", "Customer_Id", "dbo.Klienci");
            DropForeignKey("dbo.Wypozyczenia", "Books_Id", "dbo.Ksiazki");
            DropIndex("dbo.Wypozyczenia", new[] { "Customer_Id" });
            DropIndex("dbo.Wypozyczenia", new[] { "Books_Id" });
            DropTable("dbo.Wypozyczenia");
            DropTable("dbo.Ksiazki");
        }
    }
}
