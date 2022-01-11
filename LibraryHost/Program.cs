using LibraryService;
using System;
using System.ServiceModel;

namespace LibraryHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost BorrowsService = new ServiceHost(typeof(BorrowsRepository));
            ServiceHost CustomersService = new ServiceHost(typeof(CustomersRepository));
            ServiceHost BooksService = new ServiceHost(typeof(BooksRepository));
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
