namespace LibraryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _005 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Wypozyczenia", name: "Books_Id", newName: "Book_Id");
            RenameIndex(table: "dbo.Wypozyczenia", name: "IX_Books_Id", newName: "IX_Book_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Wypozyczenia", name: "IX_Book_Id", newName: "IX_Books_Id");
            RenameColumn(table: "dbo.Wypozyczenia", name: "Book_Id", newName: "Books_Id");
        }
    }
}
