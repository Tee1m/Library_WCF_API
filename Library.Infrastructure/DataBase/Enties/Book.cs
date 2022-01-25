﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace LibraryService
{
    [Table("Ksiazki")]
    public class Book : DbObject
    {
        [DataMember]
        [Required]
        public string Title { get; set; }

        [DataMember]
        [Required]
        public string AuthorName { get; set; }

        [DataMember]
        [Required]
        public string AuthorSurname { get; set; }

        [DataMember]
        [Required]
        public int Availability { get; set; }    
        
        [DataMember]
        [Required]
        public string Description { get; set; }

        public Book() {}

        public override string ToString()
        {
            return $"{Title}, {AuthorName} {AuthorSurname}";
        }
    }
}