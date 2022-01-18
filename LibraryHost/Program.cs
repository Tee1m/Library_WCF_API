using LibraryService;
using System;
using System.ServiceModel;

namespace LibraryHost
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryDbClient _libraryDb = new LibraryDbClient();

            ServiceHost BorrowsService = new ServiceHost(new BorrowsRepository(_libraryDb));
            ServiceHost CustomersService = new ServiceHost(new CustomersRepository(_libraryDb));
            ServiceHost BooksService = new ServiceHost(new BooksRepository(_libraryDb));
            Console.WriteLine("Uruchamianie ...");

            BorrowsService.Opened += BorrowsServiceOpened;
            CustomersService.Opened += CustomersServiceOpened;
            BooksService.Opened += BooksServiceOpened;

            CustomersService.Open();            
            BorrowsService.Open();
            BooksService.Open();

            Console.ReadKey();

            BorrowsService.Close();
            CustomersService.Close();
            BooksService.Close();
        }

        private static void BooksServiceOpened(object sender, EventArgs e)
        {
            Console.WriteLine("BooksRepository host został uruchomiony");
        }

        private static void CustomersServiceOpened(object sender, EventArgs e)
        {
            Console.WriteLine("CustomersRepository host został uruchomiony");
        }

        private static void BorrowsServiceOpened(object sender, EventArgs e)
        {
            Console.WriteLine("BorrowsRepository host został uruchomiony");
        }
    }
}
