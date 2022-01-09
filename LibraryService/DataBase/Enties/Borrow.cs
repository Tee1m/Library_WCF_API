using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System;

namespace LibraryService
{
    [DataContract]
    [Table("Wypozyczenia")]
    public class Borrow : DbObject
    {
        [DataMember]
        [Required]
        public Customer Customer { get; set; }
        [DataMember]
        [Required]
        public Book Books { get; set; }
        [DataMember]
        public DateTime DateOfBorrow { get; set; }
        [DataMember]
        public DateTime? Return { get; set; }

        public Borrow()
        {
            DateOfBorrow = DateTime.Today;
        }
    }
}
