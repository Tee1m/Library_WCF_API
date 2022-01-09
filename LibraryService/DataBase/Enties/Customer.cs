﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "LibraryService.ContractType".
    [DataContract]
    [Table("Klienci")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        [Required]
        public string Surname { get; set; }
        [DataMember]
        [Required]
        public string Address { get; set; }
        [DataMember]
        [Required]
        public int TelephoneNumber { get; set; }

        public Customer(string name, string surname, string address, int telephoneNumber)
        {
            this.Name = name;
            this.Surname = surname;
            this.Address = address;
            this.TelephoneNumber = telephoneNumber;
        }

        public Customer () { }
    }
}
