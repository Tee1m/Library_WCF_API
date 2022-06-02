using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Infrastructure
{
    [Table("Ksiazki")]
    public class BookDAL : DbObject
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        public string AuthorSurname { get; set; }

        [Required]
        public int Amount { get; set; }    
        
        [Required]
        public string Description { get; set; }

        public BookDAL() {}

        public override string ToString()
        {
            return $"{Title}, {AuthorName} {AuthorSurname}";
        }
    }
}
