namespace LibraryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _009 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Wypozyczenia", name: "Book_Id", newName: "BookId");
            RenameColumn(table: "dbo.Wypozyczenia", name: "Customer_Id", newName: "CustomerId");
            RenameIndex(table: "dbo.Wypozyczenia", name: "IX_Customer_Id", newName: "IX_CustomerId");
            RenameIndex(table: "dbo.Wypozyczenia", name: "IX_Book_Id", newName: "IX_BookId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Wypozyczenia", name: "IX_BookId", newName: "IX_Book_Id");
            RenameIndex(table: "dbo.Wypozyczenia", name: "IX_CustomerId", newName: "IX_Customer_Id");
            RenameColumn(table: "dbo.Wypozyczenia", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Wypozyczenia", name: "BookId", newName: "Book_Id");
        }
    }
}
