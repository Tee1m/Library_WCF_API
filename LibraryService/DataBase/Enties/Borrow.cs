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

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [DataMember]
        [Required]
        public Book Book { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [DataMember]
        public DateTime DateOfBorrow { get; set; }

        [DataMember]
        public DateTime? Return { get; set; }

        public Borrow() {}

        public Borrow(Customer customer,  Book book)
        {
            DateOfBorrow = DateTime.Now;
            this.Customer = customer;
            this.Book = book;
        }
    }
}
