using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System;

namespace LibraryService
{
    [Table("Wypozyczenia")]
    public class Borrow : DbObject
    {
        [DataMember]
        [Required]
        public Customer Customer { get; set; }
        [DataMember]
        [Required]
        public Book Book { get; set; }
        [DataMember]
        public DateTime DateOfBorrow { get; set; }
        [DataMember]
        public DateTime? Return { get; set; }
        
        public Borrow() { }

        public Borrow(Customer customer, Book book)
        {
            this.Customer = Customer;
            this.Book = Book;
            this.DateOfBorrow = DateTime.Today;
        }

        
    }
}
