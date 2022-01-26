using System;
using System.Data.Entity;
using System.Linq;

namespace Library.Infrastructure
{
    public class LibraryDb : DbContext , IUnitOfWork
    {
        // Your context has been configured to use a 'LibraryDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LibraryService.LibraryDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LibraryDb' 
        // connection string in the application configuration file.
        public LibraryDb()
            : base("name=LibraryDb")
        {
            Database.SetInitializer<LibraryDb>(new CreateDatabaseIfNotExists<LibraryDb>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Borrow> Borrows { get; set; }

        public void Add<T>(T obj) where T : class
        {
            Set<T>().Add(obj);
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public IQueryable<T> Get<T>() where T : class
        {
            return Set<T>();
        }

        public void Remove<T>(T obj) where T : class
        {
            Set<T>().Remove(obj);
        }

        public void Attach<T>(T obj) where T : class
        {
            Set<T>().Attach(obj);
        }

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}