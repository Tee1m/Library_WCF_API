using System;
using System.Runtime.Serialization;

namespace LibraryService
{
    [DataContract]
    public class BorrowDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Customer { get; set; }

        public int CustomerId { get; set; }

        [DataMember]
        public string Book { get; set; }

        public int BookId { get; set; }

        [DataMember]
        public DateTime DateOfBorrow { get; set; }

        [DataMember]
        public DateTime? Return { get; set; }

    }
}
