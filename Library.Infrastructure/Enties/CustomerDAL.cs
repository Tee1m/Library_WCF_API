using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Application.ContractType".

    [Table("Klienci")]
    public class CustomerDAL : DbObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string TelephoneNumber { get; set; }

        public CustomerDAL() {}

        public override string ToString()
        {
            return $"{Name} {Surname} {TelephoneNumber}";
        }
    }
}
