using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System;

namespace Library.Infrastructure
{
    [Table("Wypozyczenia")]
    public class Borrow : DbObject
    {
        [Required]
        public virtual Customer Customer { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        public virtual Book Book { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public DateTime DateOfBorrow { get; set; }

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
