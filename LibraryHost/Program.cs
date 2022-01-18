using LibraryService;
using System;
using System.ServiceModel;

namespace LibraryHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var _dbClient = new LibraryDbClient(new LibraryDb());

            var BorrowsService = new ServiceHost(new BorrowsService(_dbClient));
            var CustomersService = new ServiceHost(new CustomersService(_dbClient));
            var BooksService = new ServiceHost(new BooksService(_dbClient));
            
            Console.WriteLine("Uruchamianie ...");

            BorrowsService.Opened += BorrowsServiceOpened;
            CustomersService.Opened += CustomersServiceOpened;
            BooksService.Opened += BooksServiceOpened;
            
            BorrowsService.Open();
            CustomersService.Open();            
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
