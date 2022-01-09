using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService;
using System.ServiceModel;

namespace LibraryHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(LibraryAPI));

            Console.WriteLine("Uruchamianie ...");
            host.Opened += Host_Opened;
            host.Open();
            Console.ReadKey();
        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("LibraryAPI host został uruchomiony");
        }
    }
}
