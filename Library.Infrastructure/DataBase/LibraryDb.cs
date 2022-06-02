using System;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure
{
    public class LibraryDb : DbContext
    {
        // Your context has been configured to use a 'LibraryDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Application.LibraryDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LibraryDb' 
        // connection string in the application configuration file.
        public LibraryDb(string nameOrConnectionString): base(nameOrConnectionString)
        {
            Database.SetInitializer<LibraryDb>(new CreateDatabaseIfNotExists<LibraryDb>());
        }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<CustomerDAL> Customers { get; set; }
        public virtual DbSet<BookDAL> Books { get; set; }
        public virtual DbSet<BorrowDAL> Borrows { get; set; }

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}