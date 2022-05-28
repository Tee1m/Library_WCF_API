using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Borrow
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
