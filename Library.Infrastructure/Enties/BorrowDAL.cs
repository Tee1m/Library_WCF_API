using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System;

namespace DAL
{
    [Table("Wypozyczenia")]
    public class BorrowDAL : DbObject
    {
        [Required]
        public virtual CustomerDAL CustomerDAL { get; set; }

        [ForeignKey("CustomerDAL")]
        public int CustomerId { get; set; }

        [Required]
        public virtual BookDAL BookDAL { get; set; }

        [ForeignKey("BookDAL")]
        public int BookId { get; set; }

        public DateTime DateOfBorrow { get; set; }

        public DateTime? Return { get; set; }

        public BorrowDAL() {}

        public BorrowDAL(CustomerDAL customer,  BookDAL book)
        {
            DateOfBorrow = DateTime.Now;
            this.CustomerDAL = customer;
            this.BookDAL = book;
        }
    }
}
